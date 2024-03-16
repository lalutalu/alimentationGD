import re
import PyPDF2
from reportlab.pdfbase.pdfdoc import count
from reportlab.pdfgen import canvas
from reportlab.pdfbase.pdfmetrics import stringWidth
from reportlab.lib.colors import black

noCode = 0
PERCENTAGE = 14.95


# class Product:
#     def __init__(self, name, prixOg, prixNew, code39, weight, unit) -> None:
#         self.name = name
#         self.prixOg = prixOg
#         self.prixNew = prixNew
#         self.code39 = code39
#         self.weight = weight
#         self.unit = unit


def extract_product_lines(pdf_path: str) -> list:
    with open(pdf_path, 'rb') as pdf:
        reader = PyPDF2.PdfReader(pdf, strict=False)
        product_lines = []

        for page_num in range(len(reader.pages)):
            page = reader.pages[page_num]
            content = page.extract_text()

            if "BOTTIN DE COMMANDE" in content:
                lines = content.split('\n')
                start_index = next((i for i, line in enumerate(
                    lines) if "BOTTIN DE COMMANDE" in line), None)
                if start_index is not None:
                    for i in range(start_index + 1, len(lines)):
                        line = lines[i]
                        if re.match(r'^\d+', line.strip()) or re.search(r'\*\*|1/2|\s*½', line.strip()):
                            # Splitting the line after the multi-digit code
                            code_match = re.match(
                                r'^(\d+)\s+(.*)', line.strip())
                            if code_match:
                                code = code_match.group(1)
                                product_name = code_match.group(2)
                                # Concatenate the code and product name
                                product_lines.append(code + " " + product_name)
                            else:
                                # Split line based on either delimiter and iterate over sublines
                                sublines = re.split(
                                    r'(\*\*|1/2|\s*½)', line.strip())
                                for subline in sublines:
                                    if subline.strip():
                                        product_lines.append(subline.strip())
                        else:
                            # Append the line directly if it doesn't start with a code or delimiter
                            product_lines.append(line.strip())
        return product_lines


products = []
pdf_path = "bottin_epicerie_sjpj__20231214__.pdf"
combined_lines = extract_product_lines(pdf_path)

for line in combined_lines:
    print(line)

print(count(combined_lines))
# weight, unit = extract_weight(line)

# product = Product(
# name=extract_product_name(line),
# prixOg=extract_original_price(line),
# prixNew=calculate_new_price(float(extract_original_price(line))),
# code39=extract_product_code(line),
# weight=weight,
# unit=unit
# )

# products.append(product)


# output_pdf_path = "products_information.pdf"
# create_product_pdf(products, output_pdf_path)
#  for product in products:
# print("Product Name:", product.name)
# print("Original Price:", product.prixOg)
# print("New Price:", product.prixNew)
# print("Product Code:", product.code39)
# print(f"Quantity: {extract_quantity(line)}, Weight: {product.weight} {product.unit}")
# print("-----")
