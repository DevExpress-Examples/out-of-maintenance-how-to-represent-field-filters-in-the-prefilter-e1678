<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))
<!-- default file list end -->
# How to Display Field Filter In the Prefilter Dialog


> This example uses the [classic filter popup](https://docs.devexpress.com/WindowsForms/1919/controls-and-libraries/pivot-grid/data-shaping/filtering/filtering-overview) and [Prefilter dialog](https://docs.devexpress.com/WindowsForms/6226). Starting from **v17.2**, the [Excel-Style Filter Popup](https://community.devexpress.com/blogs/thinking/archive/2017/11/30/winforms-pivot-grid-excel-inspired-filter-popup-and-conditional-formatting.aspx) dialog is the default option. Its filter criteria include field filters and the code included in this example is unnecessary. 

This application handles the [PivotGridControl.FieldFilterChanged](https://docs.devexpress.com/WindowsForms/DevExpress.XtraPivotGrid.PivotGridControl.FieldFilterChanged) event to modify the [Prefilter.Criteria](https://docs.devexpress.com/WindowsForms/DevExpress.XtraPivotGrid.Prefilter.Criteria) property and display the current field's (column) filter.

![screenshot](https://github.com/DevExpress-Examples/how-to-represent-field-filters-in-the-prefilter-e1678/blob/13.1.4%2B/images/screenshot.png)