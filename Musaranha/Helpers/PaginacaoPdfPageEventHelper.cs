using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Musaranha
{
    public class PaginacaoPdfPageEventHelper : PdfPageEventHelper
    {
        PdfTemplate pageCount;
        private Font ffont = new Font(Font.FontFamily.UNDEFINED, 10);

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            pageCount = writer.DirectContent.CreateTemplate(30, 16);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable table = new PdfPTable(2);
            table.SetWidths(new float[] { 48, 2 });
            table.TotalWidth = document.PageSize.Width - document.RightMargin - document.LeftMargin;
            table.LockedWidth = true;
            table.DefaultCell.FixedHeight = 20;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(new Phrase (String.Format("Página {0} de", writer.CurrentPageNumber), ffont));
            PdfPCell cell = new PdfPCell(Image.GetInstance(pageCount));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            table.WriteSelectedRows(0, -1, 34, document.PageSize.Height - (document.TopMargin - 20), writer.DirectContent);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            ColumnText.ShowTextAligned(pageCount, Element.ALIGN_LEFT,
                    new Phrase((writer.CurrentPageNumber - 1).ToString(), ffont),
                    2, 4, 0);
        }
    }
}