// const fs = require('fs');
// const { PDFDocument } = require('pdf-lib');
// const bwipjs = require('bwip-js');
// const pdfParse = require('pdf-parse');
//
// // Read the PDF content into a buffer
// let databuffer = fs.readFileSync('./circulaire-25-janvier-au-8-fev.pdf');
//
// // Use the 'pdf-parse' library to extract text from the PDF buffer
// pdfParse(databuffer).then(async function (data) {
//     let lines = data.text.split('\n');
//     let products = [];
//
//     for (let i = 0; i < lines.length; i++) {
//         let line = lines[i];
//
//         if (line.includes('IMPACT') && line.match(/\d/)) {
//             let parts = line.split(/\s+/);
//
//             let productCode = parts[parts.length - 1].replace(/.*_______/, '');
//             let productNameParts = parts.slice(1, -2);
//             let productName = productNameParts.join(' ').trim().replace(/000.*/, '');
//
//             let netPriceIndex = parts.findIndex(part => /^\d+(\.\d+)?$/.test(part));
//
//             if (netPriceIndex !== -1 && parts.length > netPriceIndex) {
//                 let netPrice = parseFloat(parts[netPriceIndex].replace(',', ''));
//
//                 let quantityLine = lines[i + 1];
//                 let quantityMatch = quantityLine.match(/(\d+)\s+(\d+)\s+(\w+)/);
//
//                 let quantity = null;
//                 if (quantityMatch) {
//                     quantity = `${quantityMatch[1]} ${quantityMatch[2]} ${quantityMatch[3]}`;
//                 }
//
//                 productCode = productCode.replace(/D$/, '');
//
//                 let percentage = 14.95;
//                 let newPrice = netPrice * (1 + percentage / 100);
//                 newPrice = Number(newPrice.toFixed(3));
//                 newPrice = Math.round(newPrice / 0.05) * 0.05;
//
//                 if (productName && quantity && !isNaN(netPrice)) {
//                     let product = {
//                         code: productCode,
//                         name: productName,
//                         quantity: quantity,
//                         price: newPrice
//                     };
//                     products.push(product);
//                 }
//             }
//         }
//     }
//
//     // Create a new PDF document
//     const pdfDoc = await PDFDocument.create();
//     const page = pdfDoc.addPage();
//     const { width, height } = page.getSize();
//
//     // Generate a code and barcode for each product and add them to the PDF
//     for (const product of products) {
//         const productName = `Product Name: ${product.name}\n`;
//         const quantity = `Product Quantity: ${product.quantity}\n`;
//         const price = `Product Price: ${product.price}\n`;
//
//         // Add product information to the PDF
//         page.drawText(productName + quantity + price);
//
//         // Generate a Code 39 barcode for the product code
//         const barcodeData = bwipjs.toBuffer({
//             bcid: 'code39',
//             text: product.code,
//             scale: 3,
//             height: 30,
//             includetext: true,
//             textxalign: 'center',
//             font: 'code39',
//         });
//
//         // Convert barcode data to base64
//         const base64Barcode = barcodeData.toString('base64');
//
//         // Embed the barcode image as JPG
//         const pdfImage = await pdfDoc.embedJpg(Buffer.from(base64Barcode, 'base64'));
//
//         // Draw the barcode on the PDF
//         const barcodeDims = pdfImage.scale(0.5);
//         page.drawImage(pdfImage, {
//             x: width / 2 - barcodeDims.width / 2,
//             y: height - 100,
//             width: barcodeDims.width,
//             height: barcodeDims.height,
//         });
//     }
//
//     // Save the PDF to a file
//     const outputPath = 'output.pdf';
//     const pdfBytes = await pdfDoc.save();
//     fs.writeFileSync(outputPath, pdfBytes);
//
//     console.log(`PDF created at: ${outputPath}`);
// });


// const fs = require('fs');
// const PDFDocument = require('pdfkit');
// const pdf = require('pdf-parse');
// const percentage = 14.95;
// const { createCanvas } = require('canvas');
// const JsBarcode = require('jsbarcode');

// let databuffer = fs.readFileSync('./circulaire-25-janvier-au-8-fev.pdf');

// pdf(databuffer).then(function (data) {
//     let lines = data.text.split('\n');
//     let products = [];

//     // Create a single PDF document
//     const doc = new PDFDocument();
//     doc.pipe(fs.createWriteStream('all_products.pdf')); // Create a single PDF file

//     // Flag to determine whether to start processing product information
//     let startProcessing = false;

//     for (let i = 0; i < lines.length; i++) {
//         let line = lines[i];

//         // Check for the line indicating the start of product information
//         if (line.includes('DESCRIPTION DU PRODUIT')) {
//             startProcessing = true;
//             continue; // Skip the header line
//         }

//         if (startProcessing) {
//             let zeroIndex = line.indexOf('000');
//             if (zeroIndex !== -1) {
//                 let productName = line.substring(line.indexOf(' ') + 1, zeroIndex).trim();
//                 let underscoreIndex = line.indexOf('_');
//                 let barcode = line.substring(underscoreIndex + 7).trim().split(' ')[0];
//                 let originalPrice = parseFloat(line.substring(zeroIndex).trim().split(' ')[1]);

//                 if (!isNaN(originalPrice)) {
//                     let detailLine = lines[i + 1];
//                     let detailParts = detailLine.trim().split(' ');
//                     let quantite = detailParts[0];
//                     let volume = detailParts.slice(1).join(' ').trim();

//                     let product = {
//                         nom: productName,
//                         quantite: quantite,
//                         volume: volume,
//                         newPrice: originalPrice * (1 + percentage / 100),
//                         codeBarres: barcode.replace(/D/g, '')
//                     };

//                     // Generate a unique filename for the barcode image
//                     var barcodeImageFileName = `barcode_${i}.png`;

//                     // Generate the Code 39 barcode image using the product's codeBarres data
//                     const canvas = createCanvas(200, 50);
//                     JsBarcode(canvas, product.codeBarres, { format: 'CODE39' });

//                     // Save the barcode image with the unique filename
//                     const barcodeImageBuffer = canvas.toBuffer('image/png');
//                     fs.writeFileSync(barcodeImageFileName, barcodeImageBuffer);

//                     // Embed the barcode image into the PDF using the file path
//                     doc.image(barcodeImageFileName, { width: 200, height: 50 });

//                     // Remove the temporary barcode image file
//                     fs.unlinkSync(barcodeImageFileName);

//                     // Add product information to the PDF
//                     doc.text(`Product Name: ${productName}`);
//                     doc.text(`Quantity: ${quantite}`);
//                     doc.text(`Volume: ${volume}`);
//                     doc.text(`Price: $${(originalPrice * (1 + percentage / 100)).toFixed(2)}`);

//                     // Add 4 spaces as a separator between products
//                     doc.text('    ');

//                     // Check if it's the last product, and if not, add a page break
//                     if (i < lines.length - 2) {
//                         doc.moveDown();
//                     }

//                     products.push(product);
//                 }
//             }
//         }
//     }

//     doc.end();

//     console.log(products);
// });

// const fs = require('fs');
// const PDFDocument = require('pdfkit');
// const pdf = require('pdf-parse');
// const percentage = 14.95;
// const { createCanvas } = require('canvas');
// const JsBarcode = require('jsbarcode');

// let databuffer = fs.readFileSync('./circulaire-25-janvier-au-8-fev.pdf');

// pdf(databuffer).then(function (data) {
//     let lines = data.text.split('\n');
//     let products = [];

//     // Create a single PDF document
//     const doc = new PDFDocument();
//     doc.pipe(fs.createWriteStream('all_products.pdf')); // Create a single PDF file

//     let startProcessing = false;

//     for (let i = 0; i < lines.length; i++) {
//         let line = lines[i].trim();

//         if (line.match(/^IMPACT-\d+/i)) {
//             startProcessing = true;
//             i--;
//             continue;
//         }

//         if (startProcessing) {
//             let zeroIndex = line.indexOf('000');
//             if (zeroIndex !== -1) {
//                 let productName = line.substring(line.indexOf(' ') + 1, zeroIndex).trim();
//                 let underscoreIndex = line.indexOf('_');
//                 let barcode = line.substring(underscoreIndex + 7).trim().split(' ')[0];
//                 let originalPrice = parseFloat(line.substring(zeroIndex).trim().split(' ')[1]);

//                 if (!isNaN(originalPrice)) {
//                     let detailLine = lines[i + 1];
//                     let detailParts = detailLine.trim().split(' ');
//                     let quantite = detailParts[0];
//                     let volume = detailParts.slice(1).join(' ').trim();

//                     let product = {
//                         nom: productName,
//                         quantite: quantite,
//                         volume: volume,
//                         newPrice: originalPrice * (1 + percentage / 100),
//                         codeBarres: barcode.replace(/D/g, '')
//                     };

//                     // Generate a unique filename for the barcode image
//                     var barcodeImageFileName = `barcode_${i}.png`;

//                     // Generate the Code 39 barcode image using the product's codeBarres data
//                     const canvas = createCanvas(200, 50);
//                     JsBarcode(canvas, product.codeBarres, { format: 'CODE39' });

//                     // Save the barcode image with the unique filename
//                     const barcodeImageBuffer = canvas.toBuffer('image/png');
//                     fs.writeFileSync(barcodeImageFileName, barcodeImageBuffer);

//                     // Embed the barcode image into the PDF using the file path
//                     doc.image(barcodeImageFileName, { width: 200, height: 50 });

//                     // Remove the temporary barcode image file
//                     fs.unlinkSync(barcodeImageFileName);

//                     // Add product information to the PDF
//                     doc.text(`Product Name: ${productName}`);
//                     doc.text(`Quantity: ${quantite}`);
//                     doc.text(`Volume: ${volume}`);
//                     doc.text(`Price: $${(originalPrice * (1 + percentage / 100)).toFixed(2)}`);
//                     doc.text(`Barcode: ${barcode}`);

//                     // Add 4 spaces as a separator between products
//                     doc.text('    ');

//                     // Check if it's the last product, and if not, add a page break
//                     if (i < lines.length - 2) {
//                         doc.moveDown();
//                     }

//                     products.push(product);
//                 }
//             }
//         }
//     }
//     doc.end();

//     console.log(products);
// });

const fs = require('fs');
const PDFDocument = require('pdfkit');
const pdf = require('pdf-parse');
const { createCanvas } = require('canvas');
const JsBarcode = require('jsbarcode');

const percentage = 14.95;
// let databuffer = fs.readFileSync('./circulaire-25-janvier-au-8-fev.pdf');
let databuffer = fs.readFileSync('./temp.pdf');

pdf(databuffer).then(function (data) {
    let lines = data.text.split('\n');
    let products = [];

    const doc = new PDFDocument();
    doc.pipe(fs.createWriteStream('all_products.pdf'));

    lines.forEach((line, i) => {
        let trimmedLine = line.trim();
            if(line.includes('000') && trimmedLine.match(/^IMPACT-\d+/i)){
            let zeroIndex = trimmedLine.indexOf('000');
            if (zeroIndex !== -1) {
                let productName = trimmedLine.substring(trimmedLine.indexOf(' ') + 1, zeroIndex).trim();
                let underscoreIndex = trimmedLine.indexOf('_');
                let barcode = trimmedLine.substring(underscoreIndex + 7).trim().split(' ')[0];
                let originalPrice = parseFloat(trimmedLine.substring(zeroIndex).trim().split(' ')[1]);

                if (!isNaN(originalPrice)) {
                    let detailLine = lines[i + 1];
                    let [quantite, ...volumeParts] = detailLine.trim().split(' ');
                    let volume = volumeParts.join(' ').trim();

                    let product = {
                        nom: productName,
                        quantite: quantite,
                        volume: volume,
                        newPrice: originalPrice * (1 + percentage / 100),
                        codeBarres: barcode.replace(/D/g, '')
                    };

                    console.log(`Processing line ${i}: ${line}`);

                    const canvas = createCanvas(200, 50);
                    JsBarcode(canvas, product.codeBarres, { format: 'CODE39' });

                    const barcodeImageBuffer = canvas.toBuffer('image/png');
                    doc.image(barcodeImageBuffer, { width: 200, height: 50 });

                    doc.text(`Product Name: ${productName}`);
                    doc.text(`Quantity: ${quantite}`);
                    doc.text(`Volume: ${volume}`);
                    doc.text(`Price: $${(originalPrice * (1 + percentage / 100)).toFixed(2)}`);
                    doc.text(`Barcode: ${barcode}`);

                    doc.text('    ');

                    products.push(product);
                }
            }
        }
    });
    doc.end();
    console.log(products);
});
