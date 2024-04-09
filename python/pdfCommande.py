from math import prod
import re
import PyPDF2
from reportlab.pdfgen import canvas
from reportlab.pdfbase.pdfmetrics import stringWidth
from reportlab.graphics.barcode import code39
from reportlab.lib.colors import black, linearlyInterpolatedColor


class Product:
    def __init__(self, name, details, code39) -> None:
        self.name = name
        self.details = details
        self.code39 = code39

def extract_sku(text_output: list) -> list:
  sku_regex = r'SKU\s*:\s*(\d{7})'
  all_skus = []
  for line in text_output:
    sku_match = re.search(sku_regex, line)
    if sku_match:
      all_skus.append(sku_match.group(1))
  return all_skus if all_skus else ["No SKU found"]


def extract_pdf_lines(pdf_path: str) -> list:
    with open(pdf_path, 'rb') as pdf:
        reader = PyPDF2.PdfReader(pdf, strict=False)
        extracted_lines = []

        for page_num in range(len(reader.pages)):
            page = reader.pages[page_num]
            content = page.extract_text()
            lines = content.splitlines()
            extracted_lines.extend(lines)

    return extracted_lines


def extract_product_names(text_output: list) -> list:
    product_names = []
    skus = extract_sku(text_output)
    sku_index = 0

    for i, line in enumerate(text_output):
        if sku_index < len(skus) and skus[sku_index] in line:
            if i > 0 and text_output[i - 1].strip():
                product_names.append(text_output[i - 1].strip())
            sku_index += 1

    return product_names if product_names else ["No product names found"]


def extract_order_details(lines: list) -> list:
  results = []

  for line in lines:
    if "Commande #" in line:
      match = re.search(r"Commande #(.*)", line)
      if match:
        results.append(match.group(0))
      else:
        results.append("Commande # not found")
  return results


def extract_price_quantities(lines: list) -> list:
    results = []
    for line in lines:
        if not line:
            continue

        if "Commande" in line:
            split_line = line.split("Commande", 1)[0]
            results.append(split_line)
            break
        else:
            results.append(line)

    return results


def create_product(names: list, skus: list, details: list) -> list:
     products = []
     for name, code39, actual_details  in zip(names, skus, details):
        product = Product(name, code39, actual_details)
        products.append(product)
     return products


def create_product_pdf(products, order_details, output_pdf_path):
    c = canvas.Canvas(output_pdf_path)

    y_position = 750
    page_height = 800
    line_height = 20
    font_size = 10

    order_info = order_details[0]
    c.setFont("Helvetica", 18)
    c.drawString(50, y_position + 50, order_info)

    for product_index, product in enumerate(products):
        c.setFont("Helvetica", font_size)
        c.drawString(50, y_position, product.name)
        c.drawString(50, y_position - 10, product.code39)
        c.drawString(50, y_position - 20, product.details)

        barcode = code39.Standard39(product.details)
        barcode.drawOn(c, 50 + 300, y_position)

        y_position -= 3 * line_height

        if y_position <= 0:
            c.showPage()
            y_position = page_height

    c.save()


if __name__ == "__main__":
    pdf_path = "../pdfs/order2.pdf"
    new_pdf_path = "../pdfs/new_order.pdf"
    combined_lines = extract_pdf_lines(pdf_path)
    print(combined_lines)
    order_details = extract_order_details(combined_lines)
    products = []
    products = create_product(extract_product_names(combined_lines), extract_sku(combined_lines), extract_price_quantities(combined_lines))
    create_product_pdf(products, extract_order_details(combined_lines) ,new_pdf_path)
