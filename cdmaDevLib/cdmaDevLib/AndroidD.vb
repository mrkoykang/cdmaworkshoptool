'' CDMA DEV TERM
'' Copyright (c) Dillon Graham 2010-2012 Chromableed Studios
'' www.chromableedstudios.com
'' chromableedstudios ( a t ) gmail ( d o t ) com
''     
'' cdmadevterm by ¿k? with help from ajh and jh
''
'' this was originally developed as a test framework, before many 
'' things about qcdm(and programming) were understood by the author
'' please forgive some code that should never have seen the light of day ;)
''
''-------------------------------------------------------------------------------------------------------------
'' CDMA DEV TERM is released AS-IS without any warranty of any thing, blah blah blah, under the GPL v3 licence
'' check out the GPL v3 for details
'' http://www.gnu.org/copyleft/gpl.html
''-------------------------------------------------------------------------------------------------------------
''
Public Class AndroidD

    Public Function CMDAutomate()
        Dim myprocess As New Process
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        StartInfo.FileName = "cmd" 'starts cmd window
        StartInfo.RedirectStandardInput = True
        StartInfo.RedirectStandardOutput = True
        StartInfo.UseShellExecute = False 'required to redirect
        StartInfo.CreateNoWindow = True 'creates no cmd window
        myprocess.StartInfo = StartInfo
        myprocess.Start()
        Dim SR As System.IO.StreamReader = myprocess.StandardOutput
        Dim SW As System.IO.StreamWriter = myprocess.StandardInput

        ''loop through commands in list box and remove each item as it is run
        While cdmaTerm.commandsListBox.Items.Count > 0
            ''run commands from list box then remove them after running
            SW.WriteLine(cdmaTerm.commandsListBox.Items(0)) 'the command you wish to run.....
            cdmaTerm.commandsListBox.Items.RemoveAt(0)
        End While

        SW.WriteLine("exit") 'exits command prompt window
        Dim results As String
        results = SR.ReadToEnd 'returns results of the command window
        SW.Close()
        SR.Close()

        Return results
        'invokes Finished delegate, which updates textbox with the results text
        ''Invoke(Finished)
    End Function

    Public Function SendCMD(ByVal UserCmd() As String) As String
        Dim myprocess As New Process
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        StartInfo.FileName = "cmd" 'starts cmd window
        StartInfo.RedirectStandardInput = True
        StartInfo.RedirectStandardOutput = True
        StartInfo.UseShellExecute = False 'required to redirect
        StartInfo.CreateNoWindow = True 'creates no cmd window
        myprocess.StartInfo = StartInfo
        myprocess.Start()
        Dim SR As System.IO.StreamReader = myprocess.StandardOutput
        Dim SW As System.IO.StreamWriter = myprocess.StandardInput

        For Each s As String In UserCmd
            SW.WriteLine(s)
        Next
        ''run commands from list() 
        'the command you wish to run.....


        SW.WriteLine("exit") 'exits command prompt window
        Dim results As String
        results = SR.ReadToEnd 'returns results of the command window
        SW.Close()
        SR.Close()

        Return results
        'invokes Finished delegate, which updates textbox with the results text
        ''Invoke(Finished)
    End Function

    Public Function SendCMD(ByVal UserCmd As String) As String
        Dim myprocess As New Process
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        StartInfo.FileName = "cmd" 'starts cmd window
        StartInfo.RedirectStandardInput = True
        StartInfo.RedirectStandardOutput = True
        StartInfo.UseShellExecute = False 'required to redirect
        StartInfo.CreateNoWindow = True 'creates no cmd window
        myprocess.StartInfo = StartInfo
        myprocess.Start()
        Dim SR As System.IO.StreamReader = myprocess.StandardOutput
        Dim SW As System.IO.StreamWriter = myprocess.StandardInput


        ''run commands from list box then remove them after running
        SW.WriteLine(UserCmd) 'the command you wish to run.....


        SW.WriteLine("exit") 'exits command prompt window
        Dim results As String
        results = SR.ReadToEnd 'returns results of the command window
        SW.Close()
        SR.Close()

        Return results
        'invokes Finished delegate, which updates textbox with the results text
        ''Invoke(Finished)
    End Function


End Class
