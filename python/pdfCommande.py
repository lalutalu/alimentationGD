import re
import PyPDF2
import os
from reportlab.pdfgen import canvas
from reportlab.graphics.barcode import code39
from dotenv import load_dotenv
import smtplib
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart
from email.mime.base import MIMEBase
from email import encoders
import tkinter as tk
import tkinter.messagebox as messagebox
from tkinter import filedialog


load_dotenv()
pdf_path = ""
new_pdf_path = os.path.join("..", "pdfs", "new_order.pdf")
gmail_key = os.getenv("GMAIL_KEY")

class Product:
    def __init__(self, name, details, code39) -> None:
        self.name = name
        self.details = details
        self.code39 = code39

import tkinter as tk
from tkinter import filedialog, messagebox


def create_gui():
    root = tk.Tk()
    root.geometry("500x500")
    root.title("Choisir PDF pour Code 39")
    root.configure(bg="#f2f2f2")

    button_style = {
        "font": ("Arial", 14, "bold"),
        "bg": "#cc2143",
        "fg": "#f2f2f2",
        "padx": 20,
        "pady": 20,
        "relief": tk.GROOVE,
    }


    def choose_file():
        global pdf_path
        file_path = filedialog.askopenfilename(filetypes=[("PDF Files", "*.pdf")])
        if file_path:
            pdf_path = file_path

    def submit():
        if pdf_path:
            try:
                combined_lines = extract_pdf_lines(pdf_path)
                order_details = extract_order_details(combined_lines)
                products = []
                products = create_product(
                    extract_product_names(combined_lines),
                    extract_sku(combined_lines),
                    extract_price_quantities(combined_lines),
                )
                create_product_pdf(
                    products,
                    extract_order_details(combined_lines),
                    get_order_sub_total(combined_lines),
                    get_order_tax(combined_lines),
                    get_order_expedition(combined_lines),
                    get_order_total(combined_lines),
                    get_client_info(combined_lines),
                    new_pdf_path,
                )
                send_emails(new_pdf_path, order_details)
                messagebox.showinfo("Succes!", "PDF généré et envoyé avec succès!")

            except Exception as e:
                messagebox.showerror("Erreur!", f"Une erreur est survenue: {str(e)}")
        else:
            messagebox.showinfo("Erreur!", f"Veuillez choisir un PDF valide!")


    # Separate frame for buttons
    button_frame = tk.Frame(root, pady=20)
    button_frame.pack()

    # Define buttons with styling
    file_button = tk.Button(
        button_frame, text="Choisir PDF!", command=choose_file, **button_style
    )
    file_button.pack(side=tk.LEFT)

    submit_button = tk.Button(
        button_frame, text="Soumettre", command=submit, **button_style
    )
    submit_button.pack(side=tk.LEFT, padx=10)

    quit_button = tk.Button(
        button_frame, text="Quitter", command=root.destroy, **button_style
    )
    quit_button.pack(side=tk.LEFT)

    root.mainloop()

# def create_gui():
#     root = tk.Tk()
#     root.geometry("500x500")
#     root.title("Choisir PDF pour Code 39")
#
#     def choose_file():
#         global pdf_path
#         file_path = filedialog.askopenfilename(filetypes=[("PDF Files", "*.pdf")])
#         if file_path:
#             pdf_path = file_path
#
#     def submit():
#         if pdf_path:
#             try:
#                 combined_lines = extract_pdf_lines(pdf_path)
#                 order_details = extract_order_details(combined_lines)
#                 products = []
#                 products = create_product(
#                     extract_product_names(combined_lines),
#                     extract_sku(combined_lines),
#                     extract_price_quantities(combined_lines),
#                 )
#                 create_product_pdf(
#                     products,
#                     extract_order_details(combined_lines),
#                     get_order_sub_total(combined_lines),
#                     get_order_tax(combined_lines),
#                     get_order_expedition(combined_lines),
#                     get_order_total(combined_lines),
#                     get_client_info(combined_lines),
#                     new_pdf_path,
#                 )
#                 send_emails(new_pdf_path, order_details)
#                 messagebox.showinfo("Succes!", "PDF genere envoye avec succes!")
#
#             except Exception as e:
#                 messagebox.showinfo("Erreur!", f"{str(e)}")
#         else:
#             messagebox.showinfo("Erreur!", f"Veuillez Choisir Un pdf valide!")
#
#     file_button = tk.Button(root, text="Choisir pdf!", command=choose_file)
#     file_button.pack()
#
#     submit_button = tk.Button(root, text="Soumettre", command=submit)
#     submit_button.pack()
#
#     quit_button = tk.Button(root, text="Quitter", command=root.destroy)
#     quit_button.pack()
#
#     root.mainloop()


def extract_sku(text_output: list) -> list:
    sku_regex = r"SKU\s*:\s*(\d{7})"
    all_skus = []
    for line in text_output:
        sku_match = re.search(sku_regex, line)
        if sku_match:
            all_skus.append(sku_match.group(1))
    return all_skus if all_skus else ["No SKU found"]


def extract_pdf_lines(pdf_path: str) -> list:
    with open(pdf_path, "rb") as pdf:
        reader = PyPDF2.PdfReader(pdf, strict=False)
        extracted_lines = []

        for page_num in range(len(reader.pages)):
            page = reader.pages[page_num]
            content = page.extract_text()
            lines = content.splitlines()
            for line in lines:
                print(line)
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


def get_client_info(lines: list) -> list:
    total_lines = []
    for line in lines:
        if "|" in line:
            total_lines.append(line)
    return total_lines


def get_order_total(lines: list[str]) -> list[str]:
    total_lines = []
    for line in lines:
        if "Total" in line:
            total_lines.append(line)
    return total_lines


def get_order_sub_total(lines: list[str]) -> list[str]:
    total_lines = []
    for line in lines:
        if "Sous-total" in line:
            total_lines.append(line)
    return total_lines


def get_order_tax(lines: list[str]) -> list[str]:
    total_lines = []
    for line in lines:
        if "Taxe" in line:
            total_lines.append(line)
    return total_lines


def get_order_expedition(lines: list[str]) -> list[str]:
    total_lines = []
    for line in lines:
        if "Expédition" in line:
            total_lines.append(line)
    return total_lines


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
    for name, code39, actual_details in zip(names, skus, details):
        product = Product(name, code39, actual_details)
        products.append(product)
    return products


def create_product_pdf(
    products,
    order_details,
    subtotal,
    tax,
    expedition,
    total,
    client_info,
    output_pdf_path,
):
    c = canvas.Canvas(output_pdf_path)

    y_position = 750
    page_height = 800
    line_height = 20

    order_info = order_details[0]
    order_info = " ".join(order_info)
    c.setFont("Helvetica", 18)

    c.drawString(50, y_position + 50, order_info)

    realClientInfo = client_info[0]
    y_position -= line_height
    c.setFont("Helvetica", 15)
    c.drawString(50, y_position + 50, realClientInfo)

    for product_index, product in enumerate(products):
        c.setFont("Helvetica", 12)
        c.drawString(50, y_position, product.name)
        c.drawString(50, y_position - 20, product.code39)
        c.drawString(50, y_position - 40, product.details)

        barcode = code39.Standard39(product.details, barWidth=1,barHeight=35)
        barcode.drawOn(c, 350, y_position)

        y_position -= 3 * line_height

        if y_position <= 0:
            c.showPage()
            y_position = page_height

    c.setFont("Helvetica", 14)

    realSubTotal = subtotal[0]
    y_position -= line_height  # Move down one line
    c.drawString(
        350, y_position, f"{realSubTotal}"
    )  # Format subtotal with 2 decimal places

    realTax = tax[0]
    y_position -= line_height  # Move down one line
    c.drawString(350, y_position, f"{realTax}")  # Format total with 2 decimal places

    realExpedition = expedition[0]
    y_position -= line_height  # Move down one line
    c.drawString(
        350, y_position, f"{realExpedition}"
    )  # Format total with 2 decimal places

    realTotal = total[0]
    y_position -= line_height  # Move down one line
    c.drawString(350, y_position, f"{realTotal}")  # Format total with 2 decimal places

    c.save()


def send_emails(file_path, order_info):
    password = gmail_key

    smtp_port = 587
    smtp_server = "smtp.gmail.com"

    email_from = "alutalu.lukas.ecole@gmail.com"
    email_to = "commandealimentationgdonline@gmail.com"

    body = f"""
        vous avez recu une nouvelle commande!
        """
    subject = order_info
    # make a MIME object to define parts of the email
    msg = MIMEMultipart()
    msg["From"] = email_from
    msg["To"] = email_to
    msg["Subject"] =  " ".join(order_info)

    # Attach the body of the message
    msg.attach(MIMEText(body, "plain"))

    # Open the file in python as a binary
    attachment = open(file_path, "rb")  # r for read and b for binary

    # Encode as base 64
    attachment_package = MIMEBase("application", "octet-stream")
    attachment_package.set_payload((attachment).read())
    encoders.encode_base64(attachment_package)
    attachment_package.add_header(
        "Content-Disposition", "attachment; filename= " + file_path
    )
    msg.attach(attachment_package)

    # Cast as string
    text = msg.as_string()

    # Connect with the server
    print("Connecting to server...")
    TIE_server = smtplib.SMTP(smtp_server, smtp_port)
    TIE_server.starttls()
    TIE_server.login(email_from, password)
    print("Succesfully connected to server")
    print()

    # Send emails to "person" as list is iterated
    print(f"Sending email to: {email_to}...")
    TIE_server.sendmail(email_from, email_to, text)
    print(f"Email sent to: {email_to}")
    print()

    # Close the port
    TIE_server.quit()


if __name__ == "__main__":
    create_gui()
