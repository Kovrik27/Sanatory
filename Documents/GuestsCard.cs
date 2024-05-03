using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.Documents
{
    class GuestsCard
    {
        //    static void Main(string[] args) 
        //    {
        //        //Создание документа
        //        Document gucard = new Document();
        //        //Создание раздела
        //        Section s = gucard.AddSection();

        //        //Добавление данных в файл
        //        String[] Header = { "ФИО гостя", "Номер комнаты", "Период проживания" };
        //        String[] Data = { "Пушкин Алексей Анатольевич", "4", "02/04/2024 - 17/05/2024" };

        //        //Добавление таблицы
        //        Table table = s.AddTable(true);
        //        table.ResetCells(Data.Length + 1,Header.Length);

        //        //Установка первой строки как заголовка и его форматирование
        //        TableRow FRow = table.Rows[0];
        //        FRow.IsHeader = true;
        //        FRow.Height = 23;
        //        FRow.RowFormat.BackColor = Color.LightSeaGreen;
        //        for (int i = 0; i < Header.Length; i++)
        //        {
        //            //Выравнивание ячеек
        //            Paragraph p = FRow.Cells[i].AddParagraph();
        //            FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
        //            p.Format.HorizontalAlignment = HorizontalAlignment.Center;

        //            //Формат данных
        //            TextRange TR = p.AppendText(Header[i]);
        //            TR.CharacterFormat.FontName = "Calibri";
        //            TR.CharacterFormat.FontSize = 12;
        //            TR.CharacterFormat.Bold = true;
        //        }

        //        //Добавление данных в остальные строки и установка формата
        //        for (int r = 0; r < Data.Length; r++)
        //        {

        //            TableRow DataRow = table.Rows[r + 1];
        //            DataRow.Height = 20;
        //            for (int c = 0; c < Data[r].Length; c++)
        //            {
        //                DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
        //                Paragraph p2 = DataRow.Cells[c].AddParagraph();
        //                TextRange TR2 = p2.AppendText(Data[r][c]);
        //                p2.Format.HorizontalAlignment = HorizontalAlignment.Center;

        //                //Тож формат
        //                TR2.CharacterFormat.FontName = "Calibri";
        //                TR2.CharacterFormat.FontSize = 11;

        //            }

        //        }

        //        //Сохранение документа
        //        gucard.SaveToFile("GuestsCard.docx", FileFormat.Docx2013);
        //    }

        //}
    }
}

