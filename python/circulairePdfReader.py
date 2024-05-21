import re
import PyPDF2
from reportlab.pdfgen import canvas
from reportlab.pdfbase.pdfmetrics import stringWidth
from reportlab.graphics.barcode import code39
from reportlab.lib.colors import black

noCode = 0
PERCENTAGE = 14.95


class Product:
    def __init__(self, name, prixOg, prixNew, code39, weight, unit, quantity) -> None:
        self.name = name
        self.prixOg = prixOg
        self.prixNew = prixNew
        self.code39 = code39
        self.quantity = quantity
        self.weight = weight
        self.unit = unit


def extract_product_name(product_line: str) -> str:
    pattern = r"IMPACT-\d+\s+(.*?)\s+(\d{3}-\d{5}-\d{5})"
    match = re.search(pattern, product_line)

    if match:
        captured_text = match.group(1)
        result = f"{captured_text.strip()}"
        return result
    else:
        return "No Name"


def extract_product_upc(product_line: str) -> str:
    pattern = r"IMPACT-\d+\s+(.*?)\s+(\d{3}-\d{5}-\d{5})"
    match = re.search(pattern, product_line)

    if match:
        captured_text = match.group(2)
        result = f"{captured_text.strip()}"
        return result
    else:
        return "No UPC"


def extract_product_code(product_line: str) -> str:
    match = re.search(r"\b(\d{7})[A-Za-z]?\b", product_line)
    if match:
        return match.group(1)
    else:
        print(f"No Code: {product_line}")
        return "No Code"


def extract_product_with_details(pdf_path: str) -> list:
    with open(pdf_path, "rb") as pdf:
        reader = PyPDF2.PdfReader(pdf, strict=False)
        product_lines = []

        for page_num in range(len(reader.pages)):
            page = reader.pages[page_num]
            content = page.extract_text()

            lines = content.split("\n")
            product_line = ""

            for line in lines:
                if re.match(r"^IMPACT-\d+\s+", line):
                    product_line = line
                elif product_line:
                    product_line += f" {line}"
                    product_lines.append(product_line)
                    product_line = ""

        return product_lines


def extract_original_price(product_line: str) -> str:
    match = re.findall(r"\b(\d+\.\d+)\s+", product_line)

    if len(match) >= 3:
        if(product_line.__contains__("IMPACT-4") and product_line.__contains__(" VIN ")):
            return match[1]
        return match[2]
    else:
        return "No Original Price"


def extract_quantity(product_line: str) -> str:
    match = re.search(r"\d+\s+(\d+)", product_line)
    return match.group(1) if match else "No Quantity"

# def extract_quantity(product_line: str) -> str:
#   quantity_match = re.search(r"\d+\s+(?!\d+\.)(\d+)(?:\s*[A-Za-z]+)?", product_line)
#   if quantity_match:
#     first_group = quantity_match.group(1)
#     if first_group:
#         return first_group[-1]
#     else:
#             match = re.search(r"\d+\s+(\d+)", product_line)     
#             return match.group(1) if match else "No Quantity"
#   else:
#     return "No Quantity"

def extract_weight(product_line: str) -> tuple:
    match = re.search(r"\d+\s+(\d+)\s+([A-Za-z]+)", product_line)
    if match:
        return match.groups(1)
    else:
        weight_match = re.search(r"(\d+)\D*$", product_line)
        weight = weight_match.group(1) if weight_match else "No Weight"

        unit_match = re.search(r"([^\d]+)$", product_line)
        unit = unit_match.group(1).strip() if unit_match else "No Unit"

        if weight == "No Weight" and unit == "No Unit":
            print(f"No Weight or Unit: {product_line}")
        return (weight, unit)


def calculate_new_price(ogPrice: float) -> float:
    new_price = ogPrice / 0.87
    return round(new_price, 2)


def create_product_pdf(products, output_pdf_path):
    c = canvas.Canvas(output_pdf_path)

    y_position = 750
    page_height = 800
    line_height = 20
    font_size = 10
    text_field_width = 30

    for product_index, product in enumerate(products):
        c.setFont("Helvetica", font_size)

        product_info = (
            f"{product.name}, Prix: ${product.prixNew}, Quantité: {product.quantity}, "
            f"Unité: {product.weight} {product.unit}, Code39: {product.code39}"
        )
        c.drawString(50, y_position, product_info)

        barcode = code39.Standard39(product.code39)
        barcode.drawOn(c, 50 + +440, y_position)

        form = c.acroForm
        field_name = f"zip_code_{product_index}"
        form.textfield(
            name=field_name,
            tooltip=f"Zip Code for {product.name}",
            x=10,
            y=y_position,  # Adjust the y-coordinate
            width=text_field_width,
            height=font_size,
            textColor=black,
            forceBorder=True,
        )

        y_position -= 3 * line_height  # Adjust line spacing

        if y_position <= 0:
            c.showPage()
            y_position = page_height

    c.save()


products = []
pdf_path = "../pdfs/circulaire 1.pdf"
combined_lines = extract_product_with_details(pdf_path)

for line in combined_lines:
    weight, unit = extract_weight(line)

    product = Product(
        name=extract_product_name(line),
        prixOg=extract_original_price(line),
        prixNew=calculate_new_price(float(extract_original_price(line))),
        code39=extract_product_code(line),
        quantity = extract_quantity(line),
        weight=weight,
        unit=unit,
    )

    products.append(product)

output_pdf_path = "../pdfs/nouveau.pdf"
create_product_pdf(products, output_pdf_path)
for product in products:
        print("Product Name:", product.name)
        print("Quantity:", product.quantity)
        print("Weight and unit:", product.weight, " ", product.unit)
        print("New Price:", product.prixNew)
        print("Product Code:", product.code39)
        print("-----")
