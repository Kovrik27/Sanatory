using Sanatory.Model;
using System;
using System.IO;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Sanatory.Documents
{
    public class DocumentCardWord
    {
        private readonly string guestsDirectory;

        public DocumentCardWord()
        {
            guestsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "GuestsDirectory");
            Directory.CreateDirectory(guestsDirectory);
        }

        public void ExportGuestToWord(Guest guest)
        {
            string safeSurName = GetSafeFileName(guest.Surname);
            string fileName = $"Карта гостя {safeSurName}.docx";
            string fileToDirectory = Path.Combine(guestsDirectory, fileName);

            var doc = DocX.Create(fileToDirectory);

            var header = doc.InsertParagraph("Пропуск в номер");
            header.Alignment = Alignment.center;
            header.FontSize(16);
            header.Bold();
            header.SpacingAfter(20);

            var data = new[]
            {
                new[] { "Фамилия:", guest.Surname ?? "-" },
                new[] { "Имя:", guest.Name ?? "-" },
                new[] { "Отчество:", guest.Lastname ?? "-" },
                new[] { "Дата заезда:", guest.DataArrival.ToShortDateString() },
                new[] { "Дата выезда:", guest.DataOfDeparture.ToShortDateString() },
                new[] { "Комната:", guest.Room?.Number.ToString() ?? "-" }
            };

            var table = doc.InsertTable(data.Length, 2);
            table.Design = TableDesign.LightShadingAccent1;
            table.Alignment = Alignment.left;

            for (int i = 0; i < data.Length; i++)
            {
                table.Rows[i].Cells[0].Paragraphs[0].Append(data[i][0]);
                table.Rows[i].Cells[1].Paragraphs[0].Append(data[i][1]);
            }

            doc.Save();
            doc.Dispose();
        }

        private string GetSafeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "Без_фамилии";

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }

            return fileName.Trim();
        }
    }
}