// const percentage = 14.95;
// const { createCanvas } = require('canvas');
// const JsBarcode = require('jsbarcode');
// const { readFile, writeFile, unlink } = require('fs').promises;
// const fs = require('fs');
// const PDFDocument = require('pdfkit');
// const pdf = require('pdf-parse');
//
// const productsPerPage = 10;
//
// async function generatePDF() {
//     try {
//         let databuffer = await readFile('./circulaire-25-janvier-au-8-fev.pdf');
//         let data = await pdf(databuffer);
//
//         let lines = data.text.split('\n');
//         let products = [];
//
//         const doc = new PDFDocument();
//         const writeStream = fs.createWriteStream('all_products.pdf');
//         doc.pipe(writeStream);
//
//         let i = 0;
//         let productsStarted = false;
//
//         while (i < lines.length) {
//             let line = lines[i];
//             console.log(`Processing line ${i}: ${line}`);
//
//             // Check for the line that starts with "IMPACT"
//             if (line.trim().startsWith('IMPACT-')) {
//                 productsStarted = true;
//                 break;
//             }
//
//             // Increment index until we find the starting point
//             i++;
//         }
//
//         while (productsStarted && i < lines.length) {
//             let line = lines[i];
//             let zeroIndex = line.indexOf('000');
//             if (zeroIndex !== -1) {
//                 let productName = line.substring(line.indexOf(' ') + 1, zeroIndex).trim();
//                 let underscoreIndex = line.indexOf('_');
//                 let barcode = line.substring(underscoreIndex + 7).trim().split(' ')[0];
//                 let originalPrice = parseFloat(line.substring(zeroIndex).trim().split(' ')[1]);
//
//                 if (!isNaN(originalPrice)) {
//                     let detailLine = lines[i + 1];
//                     let detailParts = detailLine.trim().split(' ');
//                     let quantite = detailParts[0];
//                     let volume = detailParts.slice(1).join(' ').trim();
//
//                     let product = {
//                         nom: productName,
//                         quantite: quantite,
//                         volume: volume,
//                         newPrice: originalPrice * (1 + percentage / 100),
//                         codeBarres: barcode.replace(/D/g, '')
//                     };
//
//                     // Generate the Code 39 barcode image using the product's codeBarres data
//                     const canvas = createCanvas(200, 50);
//                     JsBarcode(canvas, product.codeBarres, { format: 'CODE39' });
//
//                     // Save the barcode image with a fixed filename
//                     const barcodeImageFileName = `barcode_${i}.png`;
//
//                     // Save the barcode image buffer
//                     await writeFile(barcodeImageFileName, canvas.toBuffer('image/png'));
//
//                     // Embed the barcode image into the PDF using the file path
//                     doc.image(barcodeImageFileName, { width: 200, height: 50 });
//
//                     // Remove the temporary barcode image file
//                     await unlink(barcodeImageFileName);
//
//                     // Add product information to the PDF
//                     doc.text(`Product Name: ${productName}`);
//                     doc.text(`Quantity: ${quantite}`);
//                     doc.text(`Volume: ${volume}`);
//                     doc.text(`Price: $${product.newPrice.toFixed(2)}`);
//
//                     // Add 4 spaces as a separator between products
//                     doc.text('    ');
//
//                     products.push(product);
//
//                     // Check if the maximum number of products per page is reached
//                     if (products.length % productsPerPage === 0) {
//                         // Add a new page after every 10 products
//                         doc.addPage();
//                     }
//
//                     console.log(`Processed product ${i}: ${productName}`);
//                 }
//             }
//
//             // Increment index even if the product is not processed
//             i++;
//         }
//
//         // End the document and wait for the finish event to ensure completion before further operations
//         await new Promise((resolve) => doc.end(resolve));
//         console.log(`Total products processed: ${products.length}`);
//         console.log(products);
//         console.log('PDF file has been written successfully.');
//     } catch (error) {
//         console.error('Error:', error);
//     }
// }
//
// generatePDF();



// const { createCanvas } = require('canvas');
// const JsBarcode = require('jsbarcode');
// const { readFile, writeFile, unlink } = require('fs').promises;
// const fs = require('fs');
// const PDFDocument = require('pdfkit');
// const pdf = require('pdf-parse');
//
// const productsPerPage = 10;
//
// async function generatePDF() {
//     try {
//         let databuffer = await readFile('./circulaire-25-janvier-au-8-fev.pdf');
//         let data = await pdf(databuffer);
//
//         let lines = data.text.split('\n');
//         let products = [];
//
//         const doc = new PDFDocument();
//         const writeStream = fs.createWriteStream('all_products.pdf');
//         doc.pipe(writeStream);
//
//         let isProductSection = false;
//
//         for (let i = 0; i < lines.length; i++) {
//             let line = lines[i].trim();
//
//             // Detect the start of the product section
//             if (line.match(/^IMPACT-\d+/) || line.match(/^\d{1,3}\s+\d{2}\/\d{2}\/\d{2}/)) {
//                 isProductSection = true;
//             }
//
//             // Process product information if within a product section
//             if (isProductSection) {
//                 // Skip irrelevant lines (you may need to adjust this based on your PDF structure)
//                 if (line === '*' || line === '**' || line.startsWith('PAGE:')) {
//                     continue;
//                 }
//
//                 console.log(`Processed product ${i}: ${line}`);
//
//
//                 // For demonstration, let's extract some data
//                 let parts = line.split(/\s+/);
//                 let productName = parts.slice(1, -8).join(' ');
//                 let barcode = parts[parts.length - 1];
//
//                 // Add product information to the PDF
//                 doc.text(`Product Name: ${productName}`);
//                 doc.text(`Barcode: ${barcode}`);
//
//                 // Add 4 spaces as a separator between products
//                 doc.text('    ');
//
//                 products.push({ productName, barcode });
//             }
//
//             // Detect the end of the product section
//             if (isProductSection && line === '') {
//                 isProductSection = false;
//             }
//         }
//
//         // End the document and wait for the finish event to ensure completion before further operations
//         await new Promise((resolve) => doc.end(resolve));
//         console.log(`Total products processed: ${products.length}`);
//         console.log(products);
//         console.log('PDF file has been written successfully.');
//     } catch (error) {
//         console.error('Error:', error);
//     }
// }
//
// generatePDF();
//


// const { createCanvas } = require('canvas');
// const JsBarcode = require('jsbarcode');
// const { readFile, writeFile, unlink } = require('fs').promises;
// const fs = require('fs');
// const PDFDocument = require('pdfkit');
// const pdf = require('pdf-parse');
//
// const productsPerPage = 10;
//
// async function generatePDF() {
//     try {
//         let databuffer = await readFile('./circulaire-25-janvier-au-8-fev.pdf');
//         let data = await pdf(databuffer);
//
//         let lines = data.text.split('\n');
//         let products = [];
//
//         const doc = new PDFDocument();
//         const writeStream = fs.createWriteStream('all_products.pdf');
//         doc.pipe(writeStream);
//
//         let isProductSection = false;
//
//         for (let i = 0; i < lines.length; i++) {
//             let line = lines[i].trim();
//
//             // Detect the start of the product section
//             if (line.match(/^IMPACT-\d+/) || line.match(/^\d{1,3}\s+\d{2}\/\d{2}\/\d{2}/)) {
//                 isProductSection = true;
//             }
//
//             // Process product information if within a product section
//             if (isProductSection) {
//                 // Skip irrelevant lines (you may need to adjust this based on your PDF structure)
//                 if (line === '*' || line === '**' || line.startsWith('PAGE:')) {
//                     continue;
//                 }
//
//                 console.log(`Processed product ${i}: ${line}`);
//
//                 // Extracting some data for demonstration
//                 let parts = line.split(/\s+/);
//                 let productName = parts.slice(1, -8).join(' ');
//                 let barcode = parts[parts.length - 1];
//
//                 // Generate the Code 39 barcode image using the product's barcode data
//                 const canvas = createCanvas(200, 50);
//                 JsBarcode(canvas, barcode, { format: 'CODE39' });
//
//                 // Save the barcode image with a fixed filename
//                 const barcodeImageFileName = `barcode_${i}.png`;
//
//                 // Save the barcode image buffer
//                 await writeFile(barcodeImageFileName, canvas.toBuffer('image/png'));
//
//                 // Embed the barcode image into the PDF using the file path
//                 doc.image(barcodeImageFileName, { width: 200, height: 50 });
//
//                 // Remove the temporary barcode image file
//                 await unlink(barcodeImageFileName);
//
//                 // Add product information to the PDF
//                 doc.text(`Product Name: ${productName}`);
//                 doc.text(`Barcode: ${barcode}`);
//
//                 // Add 4 spaces as a separator between products
//                 doc.text('    ');
//
//                 products.push({ productName, barcode });
//             }
//
//             // Detect the end of the product section
//             if (isProductSection && line === '') {
//                 isProductSection = false;
//             }
//         }
//
//         // End the document and wait for the finish event to ensure completion before further operations
//         await new Promise((resolve) => doc.end(resolve));
//         console.log(`Total products processed: ${products.length}`);
//         console.log(products);
//         console.log('PDF file has been written successfully.');
//     } catch (error) {
//         console.error('Error:', error);
//     }
// }
//
// generatePDF();


const { createCanvas } = require('canvas');
const JsBarcode = require('jsbarcode');
const { readFile, writeFile, unlink } = require('fs').promises;
const fs = require('fs');
const PDFDocument = require('pdfkit');
const pdf = require('pdf-parse');

const productsPerPage = 10;
const percentage = 14.95; // Replace with the appropriate percentage value

async function generatePDF() {
    try {
        let databuffer = await readFile('./circulaire-25-janvier-au-8-fev.pdf');
        let data = await pdf(databuffer);

        let lines = data.text.split('\n');
        let products = [];

        const doc = new PDFDocument();
        const writeStream = fs.createWriteStream('all_products.pdf');
        doc.pipe(writeStream);

        let isProductSection = false;
        let productsStarted = true; // Assuming you have a condition for starting the products section

        for (let i = 0; productsStarted && i < lines.length; i++) {
            let line = lines[i].trim();

            // Detect the start of the product section
            if (line.match(/^IMPACT-\d+/) || line.match(/^\d{1,3}\s+\d{2}\/\d{2}\/\d{2}/)) {
                isProductSection = true;
            }

            // Process product information if within a product section
            if (isProductSection) {
                // Skip irrelevant lines (you may need to adjust this based on your PDF structure)
                if (line === '*' || line === '**' || line.startsWith('PAGE:')) {
                    continue;
                }

                console.log(`Processed product ${i}: ${line}`);

                // Extracting some data for demonstration
                let parts = line.split(/\s+/);
                let productName = parts.slice(1, -8).join(' ');
	let barcode = parts[parts.length - 1].replace(/\D/g, ''); // Remove non-numeric characters
                let zeroIndex = line.indexOf('000');
                let originalPrice = parseFloat(line.substring(zeroIndex).trim().split(' ')[1]);

                // Calculate the new price based on the original price and percentage
                let newPrice = !isNaN(originalPrice) ? originalPrice * (1 + percentage / 100) : null;

                // Generate the Code 39 barcode image using the product's barcode data
                const canvas = createCanvas(200, 50);
                JsBarcode(canvas, barcode, { format: 'CODE39' });

                // Save the barcode image with a fixed filename
                const barcodeImageFileName = `barcode_${i}.png`;

                // Save the barcode image buffer
                await writeFile(barcodeImageFileName, canvas.toBuffer('image/png'));

                // Embed the barcode image into the PDF using the file path
                doc.image(barcodeImageFileName, { width: 200, height: 50 });

                // Remove the temporary barcode image file
                await unlink(barcodeImageFileName);

                // Add product information to the PDF
                doc.text(`Product Name: ${productName}`);
                doc.text(`Barcode: ${barcode}`);
                doc.text(`Original Price: $${originalPrice.toFixed(2)}`);
                doc.text(`New Price: $${newPrice ? newPrice.toFixed(2) : 'N/A'}`);

                // Add 4 spaces as a separator between products
                doc.text('    ');

                products.push({ productName, barcode, originalPrice, newPrice });
            }

            // Detect the end of the product section
            if (isProductSection && line === '') {
                isProductSection = false;
                productsStarted = false; // Assuming this is where you want to stop processing products
            }
        }

        // End the document and wait for the finish event to ensure completion before further operations
        await new Promise((resolve) => doc.end(resolve));
        console.log(`Total products processed: ${products.length}`);
        console.log(products);
        console.log('PDF file has been written successfully.');
    } catch (error) {
        console.error('Error:', error);
    }
}

generatePDF();

