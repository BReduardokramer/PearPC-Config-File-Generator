Public Class Main
    Private Sub CreateConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateConfig.Click
        On Error GoTo ErrorHandler
        Dim DataArray() As String = Split(GetData(), ",")

        If VerifyData() = True Then
            MsgBox("Incomplete data, please fill all the required fields.", MsgBoxStyle.Exclamation)
            GoTo EndEvent
        End If

        'Options contained in variables for posterior use
        Dim Resolution As String = "ppc_start_resolution = "
        Dim FullScreen As String = "ppc_start_full_screen = "
        Dim RedrawTime As String = "redraw_interval_msec = "
        Dim ComposeDialogKey As String = "key_compose_dialog = "
        Dim ChangeCDKey As String = "key_change_cd_0 = "
        Dim ReleaseMouse As String = "key_toggle_mouse_grab = "
        Dim SwitchFullScreen As String = "key_toggle_full_screen = "
        Dim BootMode As String = "prom_bootmethod = "
        Dim PROMFile As String = "prom_loadfile = "
        Dim PROMBootPath As String = "prom_env_bootpath = "
        Dim BootArgs As String = "prom_env_bootargs = "
        Dim MachArgs As String = "prom_env_machargs = "
        Dim GraphicDriver As String = "prom_driver_graphic = "
        Dim PageTable As String = "page_table_pa = "
        Dim CPUPvr As String = "cpu_pvr = "
        Dim Memory As String = "memory_size = "
        Dim MasterInstalled As String = "pci_ide0_master_installed = "
        Dim MasterImage As String = "pci_ide0_master_image = "
        Dim MasterType As String = "pci_ide0_master_type = "
        Dim SlaveInstalled As String = "pci_ide0_slave_installed = "
        Dim SlaveImage As String = "pci_ide0_slave_image = "
        Dim SlaveType As String = "pci_ide0_slave_type = "
        Dim Net3c90xInstalled As String = "pci_3c90x_installed = "
        Dim Net3c90xMac As String = "pci_3c90x_mac = "
        Dim NetRTL8139Installed As String = "pci_rtl8139_installed = "
        Dim NetRTL8139Mac As String = "pci_rtl8139_mac = "
        Dim USBInstalled As String = "pci_usb_installed = "
        Dim SerialInstalled As String = "pci_serial_installed = "
        Dim NVRAMFile As String = "nvram_file = "

        'Variables that contain the values of the options
        Dim ResolutionValue As String = DataArray(0)
        Dim FullScreenValue As Long = DataArray(1)
        Dim RedrawTimeValue As Long = DataArray(2)
        Dim ComposeDialogKeyValue As String = DataArray(3)
        Dim ChangeCDKeyValue As String = DataArray(4)
        Dim ReleaseMouseValue As String = DataArray(5)
        Dim SwitchFullScreenValue As String = DataArray(6)
        Dim BootModeValue As String = DataArray(7)
        Dim PROMFileValue As String = DataArray(8)
        Dim PROMBootPathValue As String = DataArray(9)
        Dim BootArgsValue As String = DataArray(10)
        Dim MachArgsValue As String = DataArray(11)
        Dim GraphicDriverValue As String = DataArray(12)
        Dim PageTableValue As String = DataArray(13)
        Dim CPUPvrValue As String = DataArray(14)
        Dim MemoryValue As String = DataArray(15)
        Dim MasterInstalledValue As Long = DataArray(16)
        Dim MasterImageValue As String = DataArray(17)
        Dim MasterTypeValue As String = DataArray(18)
        Dim SlaveInstalledValue As Long = DataArray(19)
        Dim SlaveImageValue As String = DataArray(20)
        Dim SlaveTypeValue As String = DataArray(21)
        Dim Net3c90xInstalledValue As Long = DataArray(22)
        Dim Net3c90xMacValue As String = DataArray(23)
        Dim NetRTL8139InstalledValue As Long = DataArray(24)
        Dim NetRTL8139MacValue As String = DataArray(25)
        Dim USBInstalledValue As Long = DataArray(26)
        Dim SerialInstalledValue As Long = DataArray(27)
        Dim NVRAMFileValue As String = DataArray(28)

        'Here is where the texfile is saved to
        'the TextBox and finally saved to a file

        'Variable for saving all the text without needing a TextBox
        Dim Config As String = ""

        'Formatting the text with a function, which returns
        'the text formatted as the documentation says

        'Emulator Resolution
        Config = FormatLine(Resolution, ResolutionValue, False)

        'Full screen (Enabled or disabled)
        Config = Config & FormatLine(FullScreen, FullScreenValue, True)

        'Image redraw time
        Config = Config & FormatLine(RedrawTime, RedrawTimeValue, True)

        'Compose dialog key
        Config = Config & FormatLine(ComposeDialogKey, ComposeDialogKeyValue, False)

        'Change CD Key
        Config = Config & FormatLine(ChangeCDKey, ChangeCDKeyValue, False)

        'Release/Capture mouse key
        Config = Config & FormatLine(ReleaseMouse, ReleaseMouseValue, False)

        'Toggle Fullscreen/Windowed modes key
        Config = Config & FormatLine(SwitchFullScreen, SwitchFullScreenValue, False)

        'Boot mode (Auto, select or force)
        Config = Config & FormatLine(BootMode, BootModeValue, False)
        
        'Bugfix: File generated does not work with PearPC running on modern systems
        'Bugfix description: If the bootmethod is force, these lines are included
        'otherwise, they're not included.
        If BootModeValue = "force" Then
            
        'PROM file (File to load in case BootMethod = Force)
        Config = Config & FormatLine(PROMFile, PROMFileValue, False)
        'PROM Booth Path (Directory where PROM/PearPC boots)(Used with PROMFile)
        Config = Config & FormatLine(PROMBootPath, PROMBootPathValue, False)
        'Boot Arguments
        Config = Config & FormatLine(BootArgs, BootArgsValue, False)
            
        End If

        'Mach Arguments
        Config = Config & FormatLine(MachArgs, MachArgsValue, False)

        'Graphic driver (video.x is recommended OS X)
        Config = Config & FormatLine(GraphicDriver, GraphicDriverValue, False)

        'Initial page table(Don't change)
        Config = Config & FormatLine(PageTable, PageTableValue, True)

        'Processor Version Register (To emulate G3 or G4)
        Config = Config & FormatLine(CPUPvr, CPUPvrValue, True)

        'Memory size (must be >= 64 MiB)
        Config = Config & FormatLine(Memory, MemoryValue, True)

        'Master drive Installed (Yes or No , 1 or 0)
        Config = Config & FormatLine(MasterInstalled, MasterInstalledValue, True)

        'Route to the Master drive image
        Config = Config & FormatLine(MasterImage, MasterImageValue, False)

        'Master Drive Type(Hard disk or CD-ROM)
        Config = Config & FormatLine(MasterType, MasterTypeValue, False)

        'Slave drive Installed (Yes or No , 1 or 0)
        Config = Config & FormatLine(SlaveInstalled, SlaveInstalledValue, True)

        'Route to the Slave drive image
        Config = Config & FormatLine(SlaveImage, SlaveImageValue, False)

        'Slave Drive Type(Hard disk or CD-ROM)
        Config = Config & FormatLine(SlaveType, SlaveTypeValue, False)

        'Network card 3c90x Installed (Yes or No)
        Config = Config & FormatLine(Net3c90xInstalled, Net3c90xInstalledValue, True)

        'MAC of this card (3c90x)
        Config = Config & FormatLine(Net3c90xMac, Net3c90xMacValue, False)

        'Network card RTL8139 installed (Yes or No)
        Config = Config & FormatLine(NetRTL8139Installed, NetRTL8139InstalledValue, True)

        'MAC of this card (RTL8139)
        Config = Config & FormatLine(NetRTL8139Mac, NetRTL8139MacValue, False)

        'PCI USB capability installed (Yes or No)
        Config = Config & FormatLine(USBInstalled, USBInstalledValue, True)

        'PCI Serial port installed installed (Yes or No)(For debugging only)
        Config = Config & FormatLine(SerialInstalled, SerialInstalledValue, True)

        'NVRAM file route
        Config = Config & FormatLine(NVRAMFile, NVRAMFileValue, False)


        'For debug purposes: Shows an MsgBox of what's in the Config variable
        Dim Proceed As Integer
        Proceed = MsgBox(Config, MsgBoxStyle.YesNo)
        If Proceed = 7 Then Exit Sub

        'Show the Save dialog for choosing the config file route
        SaveConfigFile.ShowDialog()

        If Not SaveConfigFile.FileName = "" Then
            WriteFile(Config, SaveConfigFile.FileName)
        End If

EndEvent:


ErrorHandler:

        If Err.Number <> 0 Then
            MsgBox(Err.Description)
        End If

    End Sub

    Public Sub WriteFile(ByVal Text As String, ByVal Path As String)

        'Assigning the function a variable for ease of use
        'assign properties like Route, Append text and encoding type
        Dim Write As New System.IO.StreamWriter(Path, False, System.Text.Encoding.Default)

        'Writing the Text to the config file
        Write.WriteLine(Text)
        Write.Close()
    End Sub

    Public Function FormatLine(ByVal Config As String, ByVal Value As String, ByVal NoQuotes As Boolean) As String

        Dim NewLine As String

        'This uses the given variables: First config 
        '(example: ppc_start_resolution)
        'Then The value of the config
        '(example: 640x480x32
        'And if NoQuotes is False
        'then adding quotes to thev value and a
        'line jump for the next config
        '(In case it's true, there won't be quotes)
        If NoQuotes = True Then
            NewLine = Config & Value & vbCrLf
            GoTo EndFunction
        End If

        NewLine = Config & Chr(34) & Value & Chr(34) & vbCrLf

EndFunction:
        'Returning the value of the done up
        Return NewLine

    End Function

    Public Function GetData() As String
        Dim Data As String = ""


        Dim Resolution As String = StandardResolution.SelectedItem.ToString & "x" & BitDepth.SelectedItem.ToString & "@" & Frequency.Value
        Dim FullScreen As Long
        Dim RedrawTime As Long = Me.RedrawTime.Text
        Dim ComposeDialogKey As String = Me.ComposeDialogKey.Text
        Dim ChangeCDKey As String = Me.ChangeCDKey.Text
        Dim ReleaseMouse As String = MouseToggleKey.Text
        Dim SwitchFullScreen As String = ToggleFullscreenKey.Text
        Dim BootMode As String = "auto"
        Dim PROMFile As String = Me.PROMFile.Text
        Dim PROMBootPath As String = Me.PROMBootPath.Text
        Dim BootArgs As String = BootArguments.Text
        Dim MachArgs As String = MachArguments.Text
        Dim GraphicDriver As String = Me.GraphicDriver.Text
        Dim PageTable As String = Me.PageTable.Text
        Dim CPUPvr As String = "0x00088302"
        Dim Memory As String = "0x" & (RAM.Text / 0.0000256)
        Dim MasterInstalled As Long = 0
        Dim MasterImage As String = MasterDrive.Text
        Dim MasterType As String = "hd"
        Dim SlaveInstalled As Long = 0
        Dim SlaveImage As String = SlaveDrive.Text
        Dim SlaveType As String = "cdrom"
        Dim Net3c90xInstalled As Long
        Dim Net3c90xMac As String = Mac13c90.Text & ":" & Mac23c90.Text & ":" & Mac33c90.Text & ":" & Mac43c90.Text & ":" & Mac53c90.Text & ":" & Mac63c90.Text
        Dim NetRTL8139Installed As Long
        Dim NetRTL8139Mac As String = Mac18139.Text & ":" & Mac28139.Text & ":" & Mac38139.Text & ":" & Mac48139.Text & ":" & Mac58139.Text & ":" & Mac68139.Text
        Dim USBInstalled As Long = 0
        Dim SerialInstalled As Long = 0
        Dim NVRAMFile As String = Me.NVRAMFile.Text

        If FullscreenCheck.Checked = True Then
            FullScreen = 1
        Else
            FullScreen = 0
        End If

        If BootAuto.Checked = True Then
            BootMode = "auto"
        End If
        If BootSelect.Checked = True Then
            BootMode = "select"
        End If
        If BootForce.Checked = True Then
            BootMode = "force"
        End If

        If PPCG3.Checked = True Then
            CPUPvr = "0x00088302"
        End If
        If PPCG4.Checked = True Then
            CPUPvr = "0x000c0201"
        End If

        If MasterEnabled.Checked = True Then
            MasterInstalled = 1
        Else
            MasterInstalled = 0
        End If

        Select Case MasterDriveType.SelectedIndex
            Case 0
                MasterType = "hd"
            Case 1
                MasterType = "cdrom"
            Case 2
                MasterType = "nativecdrom"
        End Select

        If SlaveEnabled.Checked = True Then
            SlaveInstalled = 1
        Else
            SlaveInstalled = 0
        End If

        Select Case SlaveDriveType.SelectedIndex
            Case 0
                SlaveType = "hd"
            Case 1
                SlaveType = "cdrom"
            Case 2
                SlaveType = "nativecdrom"
        End Select

        If Net3c90x.Checked = True Then
            Net3c90xInstalled = 1
        Else
            Net3c90xInstalled = 0
        End If

        If NetRTL8139.Checked = True Then
            NetRTL8139Installed = 1
        Else
            NetRTL8139Installed = 0
        End If

        If EnableUSB.Checked = True Then
            USBInstalled = 1
        Else
            USBInstalled = 0
        End If

        If EnableSerialPort.Checked = True Then
            SerialInstalled = 1
        Else
            SerialInstalled = 0
        End If

        Data = Resolution & "," & FullScreen & "," & RedrawTime & "," & ComposeDialogKey & "," & ChangeCDKey & "," & ReleaseMouse & "," & SwitchFullScreen & "," & BootMode & "," & PROMFile & "," & PROMBootPath & "," & BootArgs & "," & MachArgs & "," & GraphicDriver & "," & PageTable & "," & CPUPvr & "," & Memory & "," & MasterInstalled & "," & MasterImage & "," & MasterType & "," & SlaveInstalled & "," & SlaveImage & "," & SlaveType & "," & Net3c90xInstalled & "," & Net3c90xMac & "," & NetRTL8139Installed & "," & NetRTL8139Mac & "," & USBInstalled & "," & SerialInstalled & "," & NVRAMFile

        Return Data

    End Function

    Public Function VerifyData() As Boolean

        Dim Incomplete As Boolean = False

        If MasterEnabled.Checked = True Then
            If MasterDrive.Text = "" Then
                Incomplete = True
            End If
        End If

        If SlaveEnabled.Checked = True Then
            If SlaveDrive.Text = "" Then
                Incomplete = True
            End If
        End If

        If Net3c90x.Checked = True Then
            If Mac13c90.Text = "" Then
                Incomplete = True
            End If
            If Mac23c90.Text = "" Then
                Incomplete = True
            End If
            If Mac33c90.Text = "" Then
                Incomplete = True
            End If
            If Mac43c90.Text = "" Then
                Incomplete = True
            End If
            If Mac53c90.Text = "" Then
                Incomplete = True
            End If
            If Mac63c90.Text = "" Then
                Incomplete = True
            End If
        End If

        If NetRTL8139.Checked = True Then
            If Mac18139.Text = "" Then
                Incomplete = True
            End If
            If Mac28139.Text = "" Then
                Incomplete = True
            End If
            If Mac38139.Text = "" Then
                Incomplete = True
            End If
            If Mac48139.Text = "" Then
                Incomplete = True
            End If
            If Mac58139.Text = "" Then
                Incomplete = True
            End If
            If Mac68139.Text = "" Then
                Incomplete = True
            End If
        End If

        If GraphicDriver.Text = "" Then
            Incomplete = True
        End If

        If CustomResolutionCheck.Checked = True Then
            If YCustomResolution.Text = "" Then
                Incomplete = True
            End If
            If XCustomResolution.Text = "" Then
                Incomplete = True
            End If
        End If

        If NVRAMFile.Text = "" Then
            Incomplete = True
        End If

        Return Incomplete

    End Function
    Private Sub Main_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        StandardResolution.SelectedIndex = 1
        BitDepth.SelectedIndex = 1
        MasterDriveType.SelectedIndex = 0
        SlaveDriveType.SelectedIndex = 1

    End Sub

    Private Sub CustomResolutionCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CustomResolutionCheck.Click
        If CustomResolutionCheck.Checked = True Then
            StandardResolution.Visible = False
            XCustomResolution.Visible = True
            YCustomResolution.Visible = True
            Frequency.Enabled = True
        Else
            StandardResolution.Visible = True
            XCustomResolution.Visible = False
            YCustomResolution.Visible = False
            Frequency.Enabled = False
        End If
    End Sub

    Private Sub ShowAdvanced_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowAdvanced.Click

        If ShowAdvanced.Checked = True Then
            GraphicSettings.Height = 120
            DisksSettings.Height -= 20
            DisksSettings.Top += 20
            Redraw.Visible = True
            RedrawTime.Visible = True
            GraphicDriverLabel.Visible = True
            GraphicDriver.Visible = True
            BrowseDriver.Visible = True
            MasterDriveTypeBasic.Visible = False
            MasterDriveType.Visible = True
            AdvancedSettings.Visible = True
            ComposeDialogKeyLabel.Visible = True
            ComposeDialogKey.Visible = True
            EnableSerialPort.Visible = True
        Else
            GraphicSettings.Height = 100
            DisksSettings.Height += 20
            DisksSettings.Top -= 20
            Redraw.Visible = False
            RedrawTime.Visible = False
            GraphicDriverLabel.Visible = False
            GraphicDriver.Visible = False
            BrowseDriver.Visible = False
            MasterDriveTypeBasic.Visible = True
            MasterDriveType.Visible = False
            AdvancedSettings.Visible = False
            ComposeDialogKeyLabel.Visible = False
            ComposeDialogKey.Visible = False
            EnableSerialPort.Visible = False
        End If

    End Sub

    Private Sub BrowseDriver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Browser.Filter = "All files (*.*)|*.*"
        Browser.ShowDialog()
        GraphicDriver.Text = Browser.FileName
    End Sub

    Private Sub MasterBrowseDrive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MasterBrowseDrive.Click

        Select Case MasterDriveType.SelectedIndex
            Case 0
                Browser.FilterIndex = 2
                Browser.ShowDialog()
                MasterDrive.Text = Browser.FileName
            Case 1
                Browser.FilterIndex = 1
                Browser.ShowDialog()
                MasterDrive.Text = Browser.FileName
            Case 2
                SelectDrive.ShowDialog()
                MasterDrive.Text = SelectDrive.SelectedPath
        End Select

    End Sub

    Private Sub MasterEnabled_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MasterEnabled.Click

        If Not MasterEnabled.Checked = True Then
            MasterTypeLabel.Enabled = False
            MasterImageLabel.Enabled = False
            If MasterDriveType.Visible Then
                MasterDriveType.Enabled = False
            Else
                MasterDriveTypeBasic.Enabled = False
            End If
            MasterDrive.Enabled = False
            MasterBrowseDrive.Enabled = False
        Else
            MasterTypeLabel.Enabled = True
            MasterImageLabel.Enabled = True
            If MasterDriveType.Visible Then
                MasterDriveType.Enabled = True
            Else
                MasterDriveTypeBasic.Enabled = True
            End If
            MasterDrive.Enabled = True
            MasterBrowseDrive.Enabled = True
        End If

    End Sub

    Private Sub SlaveEnabled_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SlaveEnabled.Click
        If Not SlaveEnabled.Checked = True Then
            SlaveTypeLabel.Enabled = False
            SlaveImageLabel.Enabled = False
            SlaveDriveType.Enabled = False
            SlaveDrive.Enabled = False
            SlaveBrowseDrive.Enabled = False
        Else
            SlaveTypeLabel.Enabled = True
            SlaveImageLabel.Enabled = True
            SlaveDriveType.Enabled = True
            SlaveDrive.Enabled = True
            SlaveBrowseDrive.Enabled = True
        End If
    End Sub

    Private Sub SlaveBrowseDrive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SlaveBrowseDrive.Click

        Select Case SlaveDriveType.SelectedIndex
            Case 0
                Browser.FilterIndex = 2
                Browser.ShowDialog()
                SlaveDrive.Text = Browser.FileName
            Case 1
                Browser.FilterIndex = 1
                Browser.ShowDialog()
                SlaveDrive.Text = Browser.FileName
            Case 2
                SelectDrive.ShowDialog()
                SlaveDrive.Text = SelectDrive.SelectedPath
        End Select

    End Sub

    Private Sub Mac_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Mac13c90.KeyPress, Mac68139.KeyPress, Mac63c90.KeyPress, Mac58139.KeyPress, Mac53c90.KeyPress, Mac48139.KeyPress, Mac43c90.KeyPress, Mac38139.KeyPress, Mac33c90.KeyPress, Mac28139.KeyPress, Mac23c90.KeyPress, Mac18139.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub

    Private Sub Net3c90x_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Net3c90x.Click
        If Net3c90x.Checked = True Then
            Net3c90MacLabel.Enabled = True
            Mac13c90.Enabled = True
            Mac23c90.Enabled = True
            Mac33c90.Enabled = True
            Mac43c90.Enabled = True
            Mac53c90.Enabled = True
            Mac63c90.Enabled = True
        Else
            Net3c90MacLabel.Enabled = False
            Mac13c90.Enabled = False
            Mac23c90.Enabled = False
            Mac33c90.Enabled = False
            Mac43c90.Enabled = False
            Mac53c90.Enabled = False
            Mac63c90.Enabled = False
        End If
    End Sub

    Private Sub NetRTL8139_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NetRTL8139.Click
        If NetRTL8139.Checked = True Then
            Net8139MacLabel.Enabled = True
            Mac18139.Enabled = True
            Mac28139.Enabled = True
            Mac38139.Enabled = True
            Mac48139.Enabled = True
            Mac58139.Enabled = True
            Mac68139.Enabled = True
        Else
            Net8139MacLabel.Enabled = False
            Mac18139.Enabled = False
            Mac28139.Enabled = False
            Mac38139.Enabled = False
            Mac48139.Enabled = False
            Mac58139.Enabled = False
            Mac68139.Enabled = False
        End If
    End Sub

    Private Sub PageTable_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles PageTable.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub

    Private Sub ToggleFullscreenKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToggleFullscreenKey.Click
        ToggleFullscreenKey.SelectAll()
    End Sub

    Private Sub MouseToggleKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MouseToggleKey.Click
        MouseToggleKey.SelectAll()
    End Sub

    Private Sub BootForce_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BootForce.CheckedChanged
        If BootForce.Checked = True Then
            PROMFileLabel.Enabled = True
            PROMFile.Enabled = True
            PROMBootPathLabel.Enabled = True
            PROMBootPath.Enabled = True
            BootArguments.Enabled = True
            BootArgumentsLabel.Enabled = True
        Else
            PROMFileLabel.Enabled = False
            PROMFile.Enabled = False
            PROMBootPathLabel.Enabled = False
            PROMBootPath.Enabled = False
            BootArguments.Enabled = False
            BootArgumentsLabel.Enabled = False
        End If
    End Sub
    
    Private Sub MasterDriveType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MasterDriveType.SelectedIndexChanged
        MasterDriveTypeBasic.Text = MasterDriveType.SelectedItem.ToString
    End Sub
End Class
