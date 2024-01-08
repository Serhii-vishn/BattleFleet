﻿namespace BattleFleet.src.FileManager
{
    class ShipTemplateManager : FileManager
    {
        private List<string> templates;
        private string templatesFilePath;

        public ShipTemplateManager():base()
        {
            templatesFilePath = Path.Combine(savesFolderPath, "Templates.txt");
            templates = new List<string>();

            ensureFileExists (templatesFilePath);
        }

        private void ensureFileExists (string path)
        {
            if (!File.Exists(path)) 
            {
                using (File.Create(path)) { }

                string tmplName = "Master";
                List<string> templates = new List<string>()
                {
                    "5,A,FIVE_DECK,VERTICAL",
                    "8,D,ONE_DECK,HORIZONTAL",
                    "0,G,ONE_DECK,HORIZONTAL",
                    "2,J,THREE_DECK,VERTICAL",
                    "4,C,THREE_DECK,HORIZONTAL",
                    "1,B,TWO_DECK,HORIZONTAL",
                    "7,G,TWO_DECK,VERTICAL",
                    "5,H,ONE_DECK,HORIZONTAL",
                    "9,J,ONE_DECK,HORIZONTAL",
                    "6,D,TWO_DECK,HORIZONTAL"
                };
                SaveTemplate(templates, tmplName);
            }
        }

        public void SaveTemplate(List<string> template, string templateName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(templatesFilePath, FileMode.Append, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine("\nName: " + templateName);
                    foreach (var str in template)
                    {
                        writer.WriteLine(str);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error saving chat history: " + ex.Message);
            }
        }

        public List<string> GetTemplateNames()
        {
            var templateNames = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(templatesFilePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Name:"))
                        {
                            templateNames.Add(line.Replace("Name:", "").Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading template names: " + ex.Message);
            }

            return templateNames;
        }

        public List<string> GetTemplateByName(string templateName)
        {
            var template = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(templatesFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Name:") && line.Replace("Name:", "").Trim() == templateName)
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.StartsWith("Name:"))
                                {
                                    break;
                                }
                                if (!string.IsNullOrWhiteSpace(line))
                                {
                                    template.Add(line);
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading template: " + ex.Message);
            }
            return template;
        }
    }
}