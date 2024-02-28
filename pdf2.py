import re
import PyPDF2

noCode = 0

def extract_product_name(product_line: str) -> str:
    pattern = r'IMPACT-\d+\s+(.*?)\s+(\d{3}-\d{5}-\d{5})'
    match = re.search(pattern, product_line)

    if match:
        captured_text = match.group(1)
        result = f"{captured_text.strip()}"
        return result
    else:
        return "No Name"

def extract_product_upc(product_line: str) -> str:
    pattern = r'IMPACT-\d+\s+(.*?)\s+(\d{3}-\d{5}-\d{5})'
    match = re.search(pattern, product_line)

    if match:
        captured_text = match.group(2)
        result = f"{captured_text.strip()}"
        return result
    else:
        return "No UPC"

def extract_product_code(product_line: str) -> str:
    match = re.search(r'\b(\d{7})[A-Za-z]?\b', product_line)

    if match:
        return match.group(1)
    else:
        print(f"No Code: {product_line}")
        return "No Code"


def extract_product_with_details(pdf_path: str) -> [str]:
    with open(pdf_path, 'rb') as pdf:
        reader = PyPDF2.PdfReader(pdf, strict=False)
        product_lines = []

        for page_num in range(len(reader.pages)):
            page = reader.pages[page_num]
            content = page.extract_text()

            lines = content.split('\n')
            product_line = ""

            for line in lines:
                if re.match(r'^IMPACT-\d+\s+', line):
                    product_line = line
                elif product_line:
                    product_line += f" {line}"
                    product_lines.append(product_line)
                    product_line = ""

        return product_lines

def extract_original_price(product_line: str) -> str:
    match = re.search(r'\b(\d+\.\d+)\s+', product_line)
    return match.group(1) if match else "No Original Price"

def extract_quantity(product_line: str) -> str:
    match = re.search(r'\d+\s+(\d+)', product_line)
    return match.group(1) if match else "No Quantity"

def extract_weight(product_line: str) -> tuple:
    match = re.search(r'\d+\s+(\d+)\s+([A-Za-z]+)', product_line)

    if match:
        return match.groups()
    else:
        weight_match = re.search(r'(\d+)\D*$', product_line)
        weight = weight_match.group(1) if weight_match else "No Weight"

        unit_match = re.search(r'([^\d]+)$', product_line)
        unit = unit_match.group(1).strip() if unit_match else "No Unit"

        if weight == "No Weight" and unit == "No Unit":
            print(f"No Weight or Unit: {product_line}")

        return (weight, unit)


pdf_path = "circulaire-25-janvier-au-8-fev.pdf"
combined_lines = extract_product_with_details(pdf_path)
    


for line in combined_lines:
    weight, unit = extract_weight(line)
    print(extract_product_name(line))
    print("Original Price: ", extract_original_price(line))
    print(extract_product_code(line))
    print(f"Quantity: {extract_quantity(line)}, Weight: {weight} {unit}")
    print("-----")
    
    if extract_product_code(line) == "No Code":
        noCode += 1

print(noCode)
