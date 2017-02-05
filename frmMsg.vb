Public Class frmMsg

    Private Sub tmrDisp_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDisp.Tick
        Me.Close()
    End Sub

    Private Sub btnOkay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOkay.Click
        Me.Close()

    End Sub
End Class