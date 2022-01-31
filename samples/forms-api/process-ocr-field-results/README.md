# FormsAPI - Process Ocr Field Results Samples

This sample demonstrates how to process an image against a `FormSet` and retrieve the OCR
results.

`FormSet`s can be created using the `FormAssist` app included with the [FormSuite](https://www.accusoft.com/products/forms-collection/formsuite-for-structured-forms/form-suite-trial/) trial download.

The key takeaways from this sample are:

* Create a `LicenseKeychain` to be used by the `FormsAPI`
* Process an unknown image against the `FormSet`
* Iterate over the field results and retrieve the OCR results.

## Building the Sample

All samples can be built using Microsoft Visual Studio 2017 or later. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and a Solution Platform (AnyCPU), then build with Build Solution located in the Build menu. To build this sample from the command line, make sure `msbuild.exe` is in your path. Navigate to the sample directory and run the command `msbuild -t:Restore;Rebuild ProcessOcrFieldResults.sln`

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

**NOTE: The Forms Suites family of .NET components run in evaluation mode if started without a license and will periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, please contact Accusoft at info@accusoft.com.**
