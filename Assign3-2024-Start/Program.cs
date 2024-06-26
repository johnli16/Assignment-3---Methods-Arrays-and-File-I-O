﻿
// TODO: declare a constant to represent the max size of the values
// and dates arrays. The arrays must be large enough to store
// values for an entire month.
int physicalSize = 31;
int logicalSize = 0;

// TODO: create a double array named 'values', use the max size constant you declared
// above to specify the physical size of the array.
double minValue = 0.0;
double maxValue = 14.0;
double[] values = new double[physicalSize];


// TODO: create a string array named 'dates', use the max size constant you declared
// above to specify the physical size of the array.
string[] dates = new string[physicalSize];

bool goAgain = true;
while (goAgain)
{
	try
	{
		DisplayMainMenu();
		string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
		if (mainMenuChoice == "L")
			logicalSize = LoadFileValuesToMemory(dates, values);
		if (mainMenuChoice == "S")
			SaveMemoryValuesToFile(dates, values, logicalSize);
		if (mainMenuChoice == "D")
			DisplayMemoryValues(dates, values, logicalSize);
		if (mainMenuChoice == "A")
			logicalSize = AddMemoryValues(dates, values, logicalSize);
		if (mainMenuChoice == "E")
			EditMemoryValues(dates, values, logicalSize);
		if (mainMenuChoice == "Q")
		{
			goAgain = false;
			throw new Exception("Bye, hope to see you again.");
		}
		if (mainMenuChoice == "R")
		{
			while (true)
			{
				if (logicalSize == 0)
					throw new Exception("No entries loaded. Please load a file into memory");
				DisplayAnalysisMenu();
				string analysisMenuChoice = Prompt("\nEnter an Analysis Menu Choice: ").ToUpper();
				if (analysisMenuChoice == "A")
					FindAverageOfValuesInMemory(values, logicalSize);
				if (analysisMenuChoice == "H")
					FindHighestValueInMemory(values, logicalSize);
				if (analysisMenuChoice == "L")
					FindLowestValueInMemory(values, logicalSize);
				if (analysisMenuChoice == "G")
					GraphValuesInMemory(dates, values, logicalSize);
				if (analysisMenuChoice == "R")
					throw new Exception("Returning to Main Menu");
			}
		}
	}
	catch (Exception ex)
	{
		Console.WriteLine($"{ex.Message}");
	}
}

void DisplayMainMenu()
{
	Console.WriteLine("\nMain Menu");
	Console.WriteLine("L) Load Values from File to Memory");
	Console.WriteLine("S) Save Values from Memory to File");
	Console.WriteLine("D) Display Values in Memory");
	Console.WriteLine("A) Add Value in Memory");
	Console.WriteLine("E) Edit Value in Memory");
	Console.WriteLine("R) Analysis Menu");
	Console.WriteLine("Q) Quit");
}

void DisplayAnalysisMenu()
{
	Console.WriteLine("\nAnalysis Menu");
	Console.WriteLine("A) Find Average of Values in Memory");
	Console.WriteLine("H) Find Highest Value in Memory");
	Console.WriteLine("L) Find Lowest Value in Memory");
	Console.WriteLine("G) Graph Values in Memory");
	Console.WriteLine("R) Return to Main Menu");
}

string Prompt(string prompt)
{
	string response = "";
	Console.Write(prompt);
	response = Console.ReadLine();
	return response;
}

string GetFileName()
{
	string fileName = "";
	do
	{
		fileName = Prompt("Enter file name including .csv or .txt: ");
	} while (string.IsNullOrWhiteSpace(fileName));
	return fileName;
}

int LoadFileValuesToMemory(string[] dates, double[] values)
{
	string fileName = GetFileName();
	int logicalSize = 0;
	string filePath = $"./data/{fileName}";
	if (!File.Exists(filePath))
		throw new Exception($"The file {fileName} does not exist.");
	string[] csvFileInput = File.ReadAllLines(filePath);
	for (int i = 0; i < csvFileInput.Length; i++)
	{
		Console.WriteLine($"lineIndex: {i}; line: {csvFileInput[i]}");
		string[] items = csvFileInput[i].Split(',');
		for (int j = 0; j < items.Length; j++)
		{
			Console.WriteLine($"itemIndex: {j}; item: {items[j]}");
		}
		if (i != 0)
		{
			dates[logicalSize] = items[0];
			values[logicalSize] = double.Parse(items[1]);
			logicalSize++;
		}
	}
	Console.WriteLine($"Load complete. {fileName} has {logicalSize} data entries");
	return logicalSize;
}

void DisplayMemoryValues(string[] dates, double[] values, int logicalSize)
{
	if (logicalSize == 0)
		throw new Exception($"No Entries loaded. Please load a file to memory or add a value in memory");
	Console.WriteLine($"\nCurrent Loaded Entries: {logicalSize}");
	Console.WriteLine($"   Date     Value");
	for (int i = 0; i < logicalSize; i++)
		Console.WriteLine($"{dates[i]}   {values[i]}");
}

double FindHighestValueInMemory(double[] values, int logicalSize)
{
	if (logicalSize == 0)
	{
		throw new Exception("No values in memory.");
	}

	double highestValue = values[0];
	for (int i = 1; i < logicalSize; i++)
	{
		if (values[i] > highestValue)
		{
			highestValue = values[i];
		}
	}
	Console.WriteLine($"Highest Value: {highestValue}");
	return highestValue;
	//TODO: Replace this code with yours to implement this function.
}

double FindLowestValueInMemory(double[] values, int logicalSize)
{
	if (logicalSize == 0)
	{
		throw new Exception("No values in memory.");
	}

	double lowestValue = values[0];
	for (int i = 1; i < logicalSize; i++)
	{
		if (values[i] < lowestValue)
		{
			lowestValue = values[i];
		}
	}
	Console.WriteLine($"Lowest Value: {lowestValue}");
	return lowestValue;
	//TODO: Replace this code with yours to implement this function.
}

void FindAverageOfValuesInMemory(double[] values, int logicalSize)
{
	if (logicalSize == 0)
	{
		throw new Exception("No values in memory.");
	}

	double sum = 0;
	for(int i = 0; i < logicalSize; i++)
	{
		sum += values[i];
	}
	double average = sum / logicalSize;
	Console.WriteLine($"Average Value: {average}");
	//TODO: Replace this code with yours to implement this function.
}

void SaveMemoryValuesToFile(string[] dates, double[] values, int logicalSize)
{
	string fileName = Prompt("Enter file name including .csv or .txt:");
	string filePath = $"./data/{fileName}";
	if (logicalSize == 0)
		throw new Exception("No entries  loaded. Please load a file into memory");
	if (logicalSize > 1)
		Array.Sort(dates, values);
	string[] csvLines = new string[logicalSize + 1];
	csvLines[0] = "dates,values";
	for (int i = 1; i <= logicalSize; i++)
	{
		csvLines[i] = $"{dates[i - 1]},{values[i - 1].ToString()}";
	}
	File.WriteAllLines(filePath, csvLines);
	Console.WriteLine($"Save complete. {fileName} has {logicalSize} entries.");
	//TODO: Replace this code with yours to implement this function.
}

int AddMemoryValues(string[] dates, double[] values, int logicalSize)
{
	double value = 0.0;
	string dateString = "";

	dateString = Prompt("Enter date format mm-dd-yyyy: ");
	bool found = false;
	for (int i = 0; i < logicalSize; i++)
		if (dates[i].Equals(dateString))
			found = true;
	if (found == true)
		throw new Exception($"{dateString} is already in memory. Edit entry instead.");
	value = PromptDoubleBetweenMinMax($"Enter a double value", minValue, maxValue);
	dates[logicalSize] = dateString;
	values[logicalSize] = value;
	logicalSize++;
	return logicalSize;
	//TODO: Replace this code with yours to implement this function.
}

void EditMemoryValues(string[] dates, double[] values, int logicalSize)
{
	double value = 0.0;
	string dateString = "";
	int foundIndex = 0;

	if (logicalSize == 0)
		throw new Exception($"No entries loaded. Please load a file to memory or add a value in memory");
	dateString = PromptDate("Enter date format mm-dd-yyyy: ");
	bool found = false;
	for (int i = 0; i < logicalSize; i++)
		if (dates[i].Equals(dateString))
		{
			found = true;
			foundIndex = i;
		}
	if (found == false)
		throw new Exception($"{dateString} is not in memory. Add entry instead.");
	value = PromptDoubleBetweenMinMax($"Enter a double value".minValue.maxValue);
	values[foundIndex] = value;
	//TODO: Replace this code with yours to implement this function.
}

void GraphValuesInMemory(string[] dates, double[] values, int logicalSize)
{
	double maxValue = values.Max();
	const int maxGraphBarLength = 50;

	Console.WriteLine("Graph of Values: ");
	for (int 1 = 0; int < logicalSize; int++)
	{
		int barLength = (int)((values[i] / maxValue) * maxGraphBarLength);
		string bar = new string('#', barLength);
	}

	Console.WriteLine($"{dates[i]}: {bar}");
	//TODO: Replace this code with yours to implement this function.
}

void DisplaySalesChart(double[] sales, string[] dates, int countOfEntries)
{
    double maxSale = sales.Max();
    int chartHeight = 20; 
    double interval = maxSale / chartHeight;
    int labelWidth = maxSale.ToString().Length;

    
    Console.WriteLine("=== Sales for the month of February ===\n");
    Console.WriteLine("Dollars");

    
    for (double level = maxSale; level >= 0; level -= interval)
    {
        
        Console.Write(level.ToString("0").PadLeft(labelWidth) + " |");

        
        for (int day = 0; day < countOfEntries; day++)
        {
            if (sales[day] >= level)
            {
                Console.Write(" █");
            }
            else
            {
                Console.Write("  ");
            }
        }
        Console.WriteLine(); 
    }

    
    Console.Write(new string(' ', labelWidth) + " +");
    foreach (string date in dates)
    {
        Console.Write("--"); 
    }
    Console.WriteLine();

    
    Console.Write(new string(' ', labelWidth) + "  "); 
    foreach (string date in dates)
    {
        Console.Write(date.PadLeft(2, '0') + " "); 
    }
    Console.WriteLine(); 
}