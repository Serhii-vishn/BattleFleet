﻿namespace BattleFleet.src.FileManager
{
    public class ShipTemplateManager : FileManager
    {
        private readonly List<string> _templates;
        private readonly string _templatesFilePath;

        public ShipTemplateManager() : base()
        {
            _templatesFilePath = Path.Combine(savesFolderPath, "Templates.txt");
            _templates = new List<string>();

            EnsureFileExists(_templatesFilePath);
            EnsureFileNotEmpty(_templatesFilePath);
        }

        public void SaveTemplate(List<string> template, string templateName)
        {
            try
            {
                using FileStream fileStream = new(_templatesFilePath, FileMode.Append, FileAccess.Write);
                using StreamWriter writer = new(fileStream);
                writer.WriteLine("\nName: " + templateName);
                foreach (var str in template)
                {
                    writer.WriteLine(str);
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
                using StreamReader reader = new(_templatesFilePath);
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Name:"))
                        templateNames.Add(line.Replace("Name:", "").Trim());
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
                using (StreamReader reader = new(_templatesFilePath))
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

        private static void EnsureFileExists(string path)
        {
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
            }
        }

        private void EnsureFileNotEmpty(string filePath)
        {
            FileInfo fileInfo = new(filePath);
            if (fileInfo.Length == 0)
            {
                string tmplName = "Master";
                _templates.AddRange(new List<string>
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
                });
                SaveTemplate(_templates, tmplName);

                _templates.Clear();

                tmplName = "Arcade";
                _templates.AddRange(new List<string>
                {
                    "4,C,FIVE_DECK,HORIZONTAL",
                    "8,F,THREE_DECK,HORIZONTAL",
                    "3,I,THREE_DECK,VERTICAL",
                    "1,E,TWO_DECK,VERTICAL",
                    "1,I,TWO_DECK,HORIZONTAL",
                    "0,B,TWO_DECK,HORIZONTAL",
                    "5,A,ONE_DECK,VERTICAL",
                    "1,G,ONE_DECK,VERTICAL",
                    "8,D,ONE_DECK,HORIZONTAL",
                    "2,A,ONE_DECK,HORIZONTAL"
                });
                SaveTemplate(_templates, tmplName);

                _templates.Clear();

                tmplName = "Redis";
                _templates.AddRange(new List<string>
                {
                    "4,E,FIVE_DECK,VERTICAL",
                    "1,C,THREE_DECK,VERTICAL",
                    "5,A,THREE_DECK,VERTICAL",
                    "2,E,TWO_DECK,HORIZONTAL",
                    "8,G,TWO_DECK,HORIZONTAL",
                    "4,J,TWO_DECK,VERTICAL",
                    "4,G,ONE_DECK,VERTICAL",
                    "1,A,ONE_DECK,HORIZONTAL",
                    "0,H,ONE_DECK,VERTICAL",
                    "5,C,ONE_DECK,HORIZONTAL"
                });
                SaveTemplate(_templates, tmplName);

                _templates.Clear();

                tmplName = "GLORY_FOR_UKRAINE";
                _templates.AddRange(new List<string>
                {
                    "5,C,FIVE_DECK,HORIZONTAL",
                    "1,C,THREE_DECK,VERTICAL",
                    "2,E,TWO_DECK,VERTICAL",
                    "1,G,THREE_DECK,VERTICAL",
                    "0,J,ONE_DECK,HORIZONTAL",
                    "8,A,ONE_DECK,HORIZONTAL",
                    "8,I,TWO_DECK,VERTICAL",
                    "7,D,TWO_DECK,HORIZONTAL",
                    "0,A,ONE_DECK,HORIZONTAL",
                    "9,E,ONE_DECK,HORIZONTAL"
                });
                SaveTemplate(_templates, tmplName);
            }
        }
    }
}