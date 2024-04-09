import re
import PyPDF2
from reportlab.pdfgen import canvas
from reportlab.pdfbase.pdfmetrics import stringWidth
from reportlab.graphics.barcode import code39
from reportlab.lib.colors import black, linearlyInterpolatedColor


class Product:
    def __init__(self, name, prix, code39, weight, unit) -> None:
        self.name = name
        self.prix = prix
        self.code39 = code39
        self.weight = weight
        self.unit = unit

def extract_sku(text_output: list) -> list:
  """Extracts all SKUs from a list of text lines."""
  sku_regex = r'SKU\s*:\s*(\d{7})'
  all_skus = []
  for line in text_output:
    sku_match = re.search(sku_regex, line)
    if sku_match:
      all_skus.append(sku_match.group(1))
  return all_skus if all_skus else ["No SKU found"]

def extract_product_with_details(pdf_path: str) -> dict:
  """Extracts product names, SKUs, and first-line details from a PDF."""
  with open(pdf_path, 'rb') as pdf:
    reader = PyPDF2.PdfReader(pdf, strict=False)
    extracted_lines = []

    for page_num in range(len(reader.pages)):
      page = reader.pages[page_num]
      content = page.extract_text()

      lines = content.splitlines()
      extracted_lines.extend(lines)
      print(lines)

  detail_list = extract_order_details(extracted_lines)

  products = {
      "names": extract_product_names(extracted_lines),
      "skus": extract_sku(extracted_lines),
      "details": detail_list,
  }

  return products

def extract_product_names(text_output: list) -> list:
    """Extracts product names from a list of text lines, using SKUs as markers."""
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
  """Extracts 'Commande #' portion and the rest of the line."""
  results = []

  for line in lines:
    if "Commande #" in line:
      match = re.search(r"Commande #(.*)", line)
      if match:
        print(f"Matched Commande: {match.group(1)}")
        results.append(match.group(0))
      else:
        results.append("Commande # not found")
  return results


if __name__ == "__main__":
    products = extract_product_with_details("order.pdf")
    print(products)
  
