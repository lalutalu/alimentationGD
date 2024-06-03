import os
from datetime import date
import re
import PyPDF2
import tkinter as tk
from tkinter import filedialog
from tkinter import messagebox
from tkinter import font as tkfont
from reportlab.pdfgen import canvas
from reportlab.graphics.barcode import code39
from reportlab.lib.colors import black

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
    match = re.search(r"(\d+D?)\s+(\d+)\s+([A-Za-z]+)", product_line)
    if match:
        return match[1]
    else:
        matches = re.finditer(r"\d+(?:D)?\s+([0-9.]+)(?:\s+([A-Za-z]+))?", product_line)
        matches = reversed(list(matches))
        for m in matches:
            if matches:
                last_match = m
                if last_match.group(1):
                    return last_match.group(1)
                else:
                    return "No Quantity - Group 1 not found"
    return "No Quantity"    


def extract_weight(product_line: str) -> tuple:
    weight = "No Weight"
    match = re.search(r"\d+\s+(\d+)\s+([A-Za-z]+)", product_line)
    if match:
        return match.groups(1)
    else:
        weight_match = re.search(r"(\d+)\D*$", product_line)
        if weight_match:
            weight = weight_match.group(1)
        weight_match = re.search(r"(\d+\D+\d+)\D*$", product_line)
        if weight_match:
            weight = weight_match.group(1)
            numbers = weight.split()
            if len(numbers) >= 2:
                last_digits = numbers[1]
                weight = last_digits

        unit_match = re.search(r"([^\d]+)$", product_line)
        unit = unit_match.group(1).strip() if unit_match else "No Unit"
        return (weight, unit)


def calculate_new_price(ogPrice: float) -> float:
    new_price = ogPrice / 0.87
    return round(new_price, 2)

def clear_text():
    file_name_entry.config(state='normal')
    file_name_entry.delete("1.0", tk.END)
    file_name_entry.config(state='disabled')

def select_pdf_file():
    file_path = filedialog.askopenfilename(filetypes=[("Fichiers PDF", "*.pdf")])
    if file_path:
        file_name_entry.config(state='normal')
        file_name_entry.delete("1.0", tk.END)
        file_name_entry.insert("1.0", file_path)
        file_name_entry.config(state='disabled')

def create_gui():

    root = tk.Tk()
    root.title("Creation Circulaire")

    root.geometry("800x500")
    root.resizable(False, False)
    root.iconbitmap("./Logo_Icone_CercleRouge_4x.ico")

    font = tkfont.Font(family="Helvetica", size=12, weight="bold", slant="italic")
    button_font = tkfont.Font(family="Helvetica", size=10, weight="bold")

    original_image = tk.PhotoImage(file="./Logo_Horizontal_Noir_4x.png")
    resized_image = original_image.subsample(8, 8)

    container_frame = tk.Frame(root)
    container_frame.pack(fill="both", expand=True)

    image_frame = tk.Frame(container_frame)
    image_frame.place(relx=0.5, rely=0.0, anchor="ne", x=0, y=0)
    image_label = tk.Label(image_frame, image=resized_image)
    image_label.pack()

    middle_frame = tk.Frame(container_frame)
    middle_frame.place(relx=0.5, rely=0.5, anchor="center")

    file_name_label = tk.Label(middle_frame, text="Convertir circulaire métro en circulaire pour client ", font=font)
    file_name_label.grid(row=1, column=0, columnspan=2, pady=5)

    global file_name_entry
    file_name_entry = tk.Text(middle_frame, width=50, height=3, font=font, wrap="word", state="disabled")
    file_name_entry.grid(row=2, column=0, columnspan=2, pady=5)

    empty_label = tk.Label(middle_frame, text="")
    empty_label.grid(row=2, column=1, pady=5)

    select_pdf_button = tk.Button(middle_frame, text="Parcourir", command=select_pdf_file, font=button_font, bg="white")
    select_pdf_button.grid(row=3, column=0, columnspan=1, pady=5)

    create_pdf_button = tk.Button(middle_frame, text="Créer", command=lambda: parse_pdf(file_name_entry.get("1.0", tk.END).strip()), width=10, height=2 ,font=button_font, bg="green", fg="white")
    create_pdf_button.grid(row=3, column=1, columnspan=1, pady=5)

    clear_button = tk.Button(middle_frame, text="Effacer", command=clear_text, font=button_font, bg="white")
    clear_button.grid(row=2, column=3, columnspan=1, pady=5,  padx=(30, 0))

    root.mainloop()

def parse_pdf(pdf_path):
    if(pdf_path == ""):
        messagebox.showerror("Erreur!", "Veuillez choisir un fichier avant de continuer...")
        return
    combined_lines = extract_product_with_details(pdf_path)

    for line in combined_lines:
        print()
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
    today = date.today()
    formatted_date = today.strftime("%d-%m-%Y")
    desktop_dir = os.path.join(os.path.join(os.environ['USERPROFILE']), 'Desktop')
    output_file_name = f"circulaire_{formatted_date}.pdf"
    output_pdf_path = os.path.join(desktop_dir, output_file_name)
    create_product_pdf(products, output_pdf_path)
    messagebox.showinfo("Succès", f"Le circulaire fût crée au chemin: \n{output_pdf_path}", icon="info")

def create_product_pdf(products, output_pdf_path):
    c = canvas.Canvas(output_pdf_path)

    page_height = 800
    line_height = 20
    font_size = 10
    text_field_width = 40
    y_position = page_height-20 

    form = c.acroForm

    for product_index, product in enumerate(products):
        c.setFont("Helvetica", font_size)
        c.drawString(50, y_position, product.name)
        c.drawString(50, y_position - 10, f"${product.prixNew:.2f}")
        c.drawString(50, y_position - 20, f"{product.quantity}")
        c.drawString(50, y_position - 30, f"{product.weight} {product.unit}")
        c.drawString(330, y_position - 25, f"{product.code39}")

        barcode = code39.Standard39(product.code39, barHeight=25)
        barcode.drawOn(c, 300, y_position - 5)

        field_name = f"product_{product_index}"
        form.textfield(
            name=field_name,
            tooltip=product.name,
            x=50,
            y=y_position - 45,
            width=text_field_width,
            height=font_size,
            textColor=black,
            forceBorder=True,
        )
        c.line(50, y_position - 50, 400, y_position - 50)
        y_position -= 5 * line_height

        if y_position <= 0:
            c.showPage()
            y_position = page_height

    c.save()

products = []
create_gui()
