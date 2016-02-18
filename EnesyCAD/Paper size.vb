Private Sub getPaperSizes(ByVal device As String)
Dim plotSet As PlotSettings
Dim piValidator As PlotInfoValidator = New PlotInfoValidator()
Dim pConfig As PlotConfig = Autodesk.AutoCAD.PlottingServices.PlotConfigManager.SetCurrentConfig(device)
Dim psValidator As PlotSettingsValidator

If TypeOf (pConfig) Is PlotConfig Then pConfig.RefreshMediaNameList()

Dim PlotPaperList As System.Collections.Specialized.StringCollection = pConfig.CanonicalMediaNames

plotSet = New PlotSettings(True)

psValidator = PlotSettingsValidator.Current

cboPaperSize.Items.Clear()
For i As Integer = 0 To PlotPaperList.Count - 1
cboPaperSize.Items.Add(psValidator.GetLocaleMediaName(plotSet, i).ToString)
Next

End Sub

// chu thich cho doan tren
cboPrinterList.Items.Clear()

For i As Integer = 0 To Autodesk.AutoCAD.PlottingServices.PlotConfigManager.Devices.Count - 1
cboPrinterList.Items.Add(Autodesk.AutoCAD.PlottingServices.PlotConfigManager.Devices.Item(i).DeviceName)
Next

I want to select a printer from the combo and then fill a listbox with the paper sizes of that printer. 
The code in the previous post seems to get the correct count of paper sizes.
The problem is the paper size names do not match those from the acad print dialog.