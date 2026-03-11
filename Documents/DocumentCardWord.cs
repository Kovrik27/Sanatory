using Sanatory.Model;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
            string fileName = $"Карта гостя: {guest.Lastname}";
            string fileToDirectory = Path.Combine(guestsDirectory, fileName);

            Document doc = new Document();
            Section section = doc.AddSection();
            Paragraph headerParagraph = section.AddParagraph();
            headerParagraph.AppendText("Пропуск в номер");

            Table table = section.AddTable();
            table.ResetCells(6, 2);

            AddRowToTable(table, 0, "Фамилия:", guest.Lastname);
            AddRowToTable(table, 1, "Имя:", guest.Name);
            AddRowToTable(table, 2, "Отчество:", guest.Surname);
            AddRowToTable(table, 3, "Дата заезда:", guest.DataArrival.ToShortDateString());
            AddRowToTable(table, 4, "Дата выезда:", guest.DataOfDeparture.ToShortDateString());
            AddRowToTable(table, 5, "Комната:", guest.Room.Number.ToString());
            
            doc.SaveToFile(fileToDirectory, FileFormat.Docx2019);
        }

        private void AddRowToTable(Table table, int rowIndex, string label, string value)
        {
            table.Rows[rowIndex].Cells[0].AddParagraph().AppendText(label);
            table.Rows[rowIndex].Cells[1].AddParagraph().AppendText(value ?? "-");
        }
    }
}
