// Copyright Accusoft Corporation

namespace ProcessOcrFieldResults
{
    using System;
    using Accusoft.Forms;

    class Program
    {
        private const string InputFormSet = @"..\..\..\..\..\inputs\Assorted Forms\Form Set\Assorted Forms.frs";
        private const string InputImage = @"..\..\..\..\..\inputs\Assorted Forms\Filled Images\Direct Deposit Form Filled.tif";

        static void Main()
        {
            // Create the required license key chain. This holds the licenses required at runtime.
            // The default is evaluation licenses.
            Accusoft.Forms.LicenseKeychain licenseKeychain = new LicenseKeychain();
            AddLicenseKeys(licenseKeychain);

            // Create a Form Process with a predefined FormSet and the license key chain.
            // FormSets can be created using the FormAssist app provided with FormSuite.
            using (Accusoft.Forms.Processor processor = new Accusoft.Forms.Processor(InputFormSet, licenseKeychain))
            {
                // Load an image for processing
                using (System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(InputImage))
                {
                    // Process the image and get the result.
                    Accusoft.Forms.FormResult formResult = processor.ProcessImage(bitmap);

                    Console.WriteLine("Identification Result State:" + formResult.IdentificationResult.State);

                    // Check the Identification results, if a match was found process the BestMatch
                    if (formResult.IdentificationResult.State != IdentificationState.NoMatchFound)
                    {
                        var bestMatch = formResult.IdentificationResult.BestMatch;
                        Console.WriteLine("Identification match confidence: " + bestMatch.MatchConfidence);
                        Console.WriteLine("--------------------------------------------------");

                        // Loop through the results looking for OCR results.
                        foreach (var fieldResult in formResult.FieldResults)
                        {
                            // Test for an OCR result 
                            if (fieldResult.Result is Accusoft.SmartZoneOCRSdk.TextBlockResult ocrResult)
                            {
                                // Write out the top level results information.
                                WriteOcrTextBlockResult(fieldResult, ocrResult);

                                // Write the result for each text line.
                                for (int lineIndex = 0; lineIndex < ocrResult.NumberTextLines; lineIndex++)
                                {
                                    Accusoft.SmartZoneOCRSdk.TextLineResult textLineResult = ocrResult.TextLine(lineIndex);
                                    WriteTextLineResult(lineIndex, textLineResult);

                                    // Write the result for each character in the result.
                                    for (int charIndex = 0; charIndex < textLineResult.NumberCharacters; charIndex++)
                                    {
                                        Accusoft.SmartZoneOCRSdk.CharacterResult characterResult = textLineResult.Character(charIndex);
                                        WriteCharacterResult(charIndex, characterResult);
                                    }

                                    Console.WriteLine();
                                }

                                Console.WriteLine("--------------------------------------------------");
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Write out a SmartZoneOcr character result.
        /// </summary>
        /// <param name="charIndex">Index of the character being written.</param>
        /// <param name="characterResult">SmartZoneOCR character result</param>
        private static void WriteCharacterResult(int charIndex, Accusoft.SmartZoneOCRSdk.CharacterResult characterResult)
        {
            Console.Write(string.Format("Character #: {0} \'{1}\'  Confidence: {2}", charIndex, characterResult.Text, characterResult.Confidence));

            // Print the alternative character results
            if (characterResult.NumberResults > 1)
            {
                Console.Write("  Alternative Characters: [");
                for (int j = 1; j < characterResult.NumberResults; j++)
                {
                    Console.Write(string.Format("(\'{0}\' Confidence: {1})", characterResult.AlternateText(j), characterResult.AlternateConfidence(j)));
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Write out a SmartZoneOcr text line result
        /// </summary>
        /// <param name="lineIndex">Index of the line being written</param>
        /// <param name="textLineResult">SmartZoneOcr Text line result</param>
        private static void WriteTextLineResult(int lineIndex, Accusoft.SmartZoneOCRSdk.TextLineResult textLineResult)
        {
            Console.WriteLine("Text Line #: {0}  Confidence: {1}  Character Count: {2} ", lineIndex, textLineResult.Confidence, textLineResult.NumberCharacters);
            Console.WriteLine("Line Text:");
            Console.WriteLine(textLineResult.Text);
            Console.WriteLine();
        }

        /// <summary>
        /// Write out a Forms field and  SmartZoneOcr text block result
        /// </summary>
        /// <param name="fieldResult">Forms field result</param>
        /// <param name="ocrTextBlock">SmartZoneOCR ocrResult</param>
        private static void WriteOcrTextBlockResult(Accusoft.Forms.FieldResult fieldResult, Accusoft.SmartZoneOCRSdk.TextBlockResult ocrTextBlock)
        {
            Console.WriteLine("OCR Field Name: \'{0}\'  Text Confidence: {1}  Line Count: {2}", fieldResult.FieldName, ocrTextBlock.Confidence, ocrTextBlock.NumberTextLines);
            Console.WriteLine("Text:");
            Console.WriteLine(ocrTextBlock.Text);
            Console.WriteLine();
        }

        /// <summary>
        /// Add Accusoft runtime licenses to the license key chain.
        /// </summary>
        /// <param name="licenseKeychain">Key change to add licenses</param>
        private static void AddLicenseKeys(Accusoft.Forms.LicenseKeychain licenseKeychain)
        {
            // Add you Accusoft provided licensing information here.
            /*
            licenseKeychain.FormFix.SetSolutionName("YourSolutionName");
            licenseKeychain.FormFix.SetSolutionKey(12345, 12345, 12345, 12345);
            licenseKeychain.FormFix.SetOEMLicenseKey("1.0.AStringForOEMLicensingContactAccusoftSalesForMoreInformation...");

            licenseKeychain.ScanFix.SetSolutionName("YourSolutionName");
            licenseKeychain.ScanFix.SetSolutionKey(12345, 12345, 12345, 12345);
            licenseKeychain.ScanFix.SetOEMLicenseKey("1.0.AStringForOEMLicensingContactAccusoftSalesForMoreInformation...");

            licenseKeychain.SmartZoneIcr.SetSolutionName("YourSolutionName");
            licenseKeychain.SmartZoneIcr.SetSolutionKey(12345, 12345, 12345, 12345);
            licenseKeychain.SmartZoneIcr.SetOEMLicenseKey("1.0.AStringForOEMLicensingContactAccusoftSalesForMoreInformation...");

            licenseKeychain.SmartZoneOcr.SetSolutionName("YourSolutionName");
            licenseKeychain.SmartZoneOcr.SetSolutionKey(12345, 12345, 12345, 12345);
            licenseKeychain.SmartZoneOcr.SetOEMLicenseKey("1.0.AStringForOEMLicensingContactAccusoftSalesForMoreInformation...");
            */
        }
    }
}
