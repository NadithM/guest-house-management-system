Imports MySql.Data.MySqlClient


Public Class Form1


    Dim cn As New MySqlConnection
    Dim cmd As MySqlCommand

    Dim flag As Integer
    Dim flagstore As Integer


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        flag = 0
        flagstore = 0

        Panel1.Visible = False
        Panel2.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel3.Visible = False
        exi_rad.Checked = True

        cn = New MySqlConnection
        cn.ConnectionString = "server=localhost;userid=root;database=test"
        Dim reader As MySqlDataReader

        Try
            cn.Open()
            Dim Query As String
            Query = "select item from itemlist"
            cmd = New MySqlCommand(Query, cn)
            reader = cmd.ExecuteReader
            Dim count As Integer
            While reader.Read
                ComboBox1.Items.Add(reader.GetString(0))
            End While
            reader.Close()
            cn.Close()
        Catch ex As MySqlException
            MsgBox(ex.Message)
        Finally
            cn.Dispose()
        End Try




    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ComboBox1.SelectedValue = 0

    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Close()

    End Sub

    Private Sub gue_hou_b_Click(sender As Object, e As EventArgs) Handles gue_hou_b.Click

        If ComboBox4.SelectedIndex = -1 Then

            MsgBox("Select A Guest House Location")
        Else



            If flag = 0 Then
                flag = 1
                Panel1.Visible = True
                Panel2.Visible = True
                Panel4.Visible = True
                Panel3.Visible = True
                gue_hou_b.BackColor = Color.Red
                gue_hou_b.Text = "Depart"


                cn = New MySqlConnection
                cn.ConnectionString = "server=localhost;userid=root;database=test"
                Dim reader As MySqlDataReader


                Try

                    cn.Open()
                    Dim Query As String
                    Query = "select * from roomd where location='" & ComboBox4.SelectedItem.ToString & "'"
                    cmd = New MySqlCommand(Query, cn)
                    reader = cmd.ExecuteReader
                    Dim count As Integer

                    While reader.Read

                        trv.Text = reader.GetString(2)
                        arv.Text = reader.GetString(3)
                        frv.Text = reader.GetString(4)

                    End While

                    reader.Close()

                    Query = "select * from roomitem where location='" & ComboBox4.SelectedItem.ToString & "'"

                    cmd = New MySqlCommand(Query, cn)
                    Dim Adpt As New MySqlDataAdapter(Query, cn)
                    Dim ds As New DataSet()
                    Adpt.Fill(ds, "roomitem")
                    DataGridView1.DataSource = ds.Tables(0)


                    cn.Close()



                Catch ex As MySqlException
                    MsgBox(ex.Message)


                Finally
                    cn.Dispose()


                End Try

            Else

                flag = 0
                If flagstore = 1 Then
                    flagstore = 0
                    Panel5.Visible = False


                    sto_b.BackColor = Color.Green
                    sto_b.Text = "In"


                End If
                Panel1.Visible = False

                Panel2.Visible = False

                Panel4.Visible = False
                Panel3.Visible = False
                gue_hou_b.BackColor = Color.Green
                gue_hou_b.Text = "Visit"

                DataGridView1.DataSource = vbNull
            End If




        End If





    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles sto_b.Click
        If flagstore = 0 Then
            flagstore = 1
            Panel5.Visible = True

            sto_b.BackColor = Color.Red
            sto_b.Text = "Out"







        Else

            flagstore = 0
            Panel5.Visible = False


            sto_b.BackColor = Color.Green
            sto_b.Text = "In"


        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Query As String
        Dim count As Integer
        count = 0

        If ComboBox1.SelectedIndex = -1 And new_rad.Checked = False And TextBox1.Text = "" Then
            MsgBox("You Have Two Option : " & vbCrLf & " " & vbTab & "1. Select the item that has been bought from the list " & vbCrLf & " " & vbTab & "2.Type the new item that has been bought" & vbCrLf & "Then check the radio button accordingly")
        ElseIf exi_rad.Checked = True Then
            If ComboBox1.SelectedIndex = -1 Then
                MsgBox("Select the item from the list")
            Else
                If TextBox2.Text = "" Then
                    MsgBox("Enter the no of " & ComboBox1.SelectedItem.ToString & " has been bought")
                Else
                    cn = New MySqlConnection
                    cn.ConnectionString = "server=localhost;userid=root;database=test"
                    Dim reader As MySqlDataReader
                    Try
                        cn.Open()
                        Query = "select No_of_Items from storage where location='" & ComboBox4.SelectedItem.ToString & "' and item='" & ComboBox1.SelectedItem.ToString & "'"
                        Console.Write(Query)
                        cmd = New MySqlCommand(Query, cn)
                        reader = cmd.ExecuteReader


                        While reader.Read

                            count = reader.GetString(0)

                        End While

                        reader.Close()

                        count = count + Integer.Parse(TextBox2.Text)


                        '  Console.Write(count.ToString)



                        Query = "update storage set no_of_items=" & count & " where location='" & ComboBox4.SelectedItem.ToString & "' and item='" & ComboBox1.SelectedItem.ToString & "'"

                        'Console.Write(Query)

                        cmd = New MySqlCommand(Query, cn)

                        cmd.ExecuteNonQuery()


                        MsgBox("" & TextBox2.Text & " " & ComboBox1.SelectedItem.ToString & " are added to the Store Room")




                        Dim com As String = "Select Item as Item ,No_of_items as Quantity  from test.storage where location='" & ComboBox4.SelectedItem.ToString & "'"
                        Console.Write(com)
                        Dim Adpt As New MySqlDataAdapter(com, cn)
                        Dim ds As New DataSet()
                        Adpt.Fill(ds, "storage")

                        DataGridView1.DataSource = ds.Tables(0)

                        cn.Close()

                    Catch ex As MySqlException
                        MsgBox(ex.Message)


                    Finally
                        cn.Dispose()


                    End Try

                End If
            End If

        ElseIf new_rad.Checked = True Then

            If TextBox1.Text = "" Then
                MsgBox("Type the Name of item to be newly added ")
            Else
                If TextBox2.Text = "" Then
                    MsgBox("Enter the no of " & TextBox1.Text & " has been bought")

                Else

                    cn = New MySqlConnection
                    cn.ConnectionString = "server=localhost;userid=root;database=test"
                    Dim reader As MySqlDataReader


                    Try

                        cn.Open()



                        Query = "INSERT INTO storage ('Location','Item', 'No_of_items') VALUES ('" & ComboBox4.SelectedText & "','" & TextBox1.Text & "', '" & TextBox2.Text & "')"
                        cmd = New MySqlCommand(Query, cn)

                        MsgBox("" & TextBox2.Text & " " & TextBox1.Text & "are added to the Store Room")



                        Query = "SELECT COUNT(item_id) FROM itemlist;"


                        cmd = New MySqlCommand(Query, cn)

                        reader = cmd.ExecuteReader


                        While reader.Read


                            count = reader.GetString(0)



                        End While

                        reader.Close()

                        Console.Write(count)


                        count = count + 1
                        Console.Write(count)

                        Query = "INSERT INTO itemist ('item_id','item') VALUES ('" & count & "','" & TextBox1.Text & "')"
                        cmd = New MySqlCommand(Query, cn)




                        Dim com As String = "Select Item as Item ,No_of_items as Quantity  from storage where location='" & ComboBox4.SelectedItem.ToString & "'"
                        Console.Write(com)
                        Dim Adpt As New MySqlDataAdapter(com, cn)
                        Dim ds As New DataSet()
                        Adpt.Fill(ds, "storage")

                        DataGridView1.DataSource = ds.Tables(0)

                        cn.Close()

                    Catch ex As MySqlException
                        MsgBox(ex.Message)


                    Finally
                        cn.Dispose()


                    End Try

                End If
            End If

        End If











    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged



        Try


            cn.Open()
            Dim com As String = "Select Item as Item ,No_of_items as Quantity  from storage where location='" & ComboBox4.SelectedItem.ToString & "'"
            Console.Write(com)
            Dim Adpt As New MySqlDataAdapter(com, cn)
            Dim ds As New DataSet()
            Adpt.Fill(ds, "storage")

            DataGridView1.DataSource = ds.Tables(0)




            cn.Close()

        Catch ex As MySqlException
            MsgBox(ex.Message)


        Finally
            cn.Dispose()


        End Try









    End Sub

    Private Sub inv_man_pan_Paint(sender As Object, e As PaintEventArgs) Handles inv_man_pan.Paint

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub exi_rad_CheckedChanged(sender As Object, e As EventArgs) Handles exi_rad.CheckedChanged



    End Sub

    Private Sub new_rad_CheckedChanged(sender As Object, e As EventArgs) Handles new_rad.CheckedChanged



    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim count As Integer
        Dim count1 As Integer
        count = 0
        count1 = 0
        Label13.Text = "Room no. " & TextBox3.Text & " items"
        Button5.Text = "Replace items in " & vbCrLf & "room no. " & TextBox3.Text & ""
        cn = New MySqlConnection
        cn.ConnectionString = "server=localhost;userid=root;database=test"
        Dim reader As MySqlDataReader
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ListBox1.Items.Clear()
        ListBox4.Items.Clear()
        Try

            cn.Open()
            Dim Query As String
            Query = "show columns in roomitem"
            cmd = New MySqlCommand(Query, cn)
            reader = cmd.ExecuteReader

            While reader.Read


                count = count + 1

                If count >= 5 Then
                    ListBox1.Items.Add(reader.GetString(0))


                End If



            End While

            reader.Close()


            Dim iter As Integer
            iter = 5
            Query = "select * from roomitem where room_no='" & TextBox3.Text & "' and location='" & ComboBox4.SelectedItem.ToString & "'"
            cmd = New MySqlCommand(Query, cn)
            reader = cmd.ExecuteReader

            While reader.Read

                While iter <= count
                    ListBox3.Items.Add(reader.GetString(iter - 1))
                    iter = iter + 1

                End While


            End While

            reader.Close()



            Query = "select item,no_of_items from storage where location='" & ComboBox4.SelectedItem.ToString & "'"
            cmd = New MySqlCommand(Query, cn)
            reader = cmd.ExecuteReader

            While reader.Read

                ListBox2.Items.Add(reader.GetString(0))
                ListBox4.Items.Add(reader.GetString(1))

            End While

            reader.Close()


            cn.Close()



        Catch ex As MySqlException
            MsgBox(ex.Message)


        Finally
            cn.Dispose()


        End Try












    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        ListBox3.SelectedIndex = ListBox1.SelectedIndex



    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        ListBox4.SelectedIndex = ListBox2.SelectedIndex
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim index As Integer
        Dim moveitemcount As Integer
        Dim index2 As Integer

        Dim str As String
        index = 0
        moveitemcount = 0
        index2 = 0

        str = ListBox3.SelectedItem.ToString()
        moveitemcount = Integer.Parse(str)

        If moveitemcount >= 1 Then
            moveitemcount = moveitemcount - 1
            ListBox3.Items(ListBox3.SelectedIndex) = moveitemcount

            index2 = ListBox2.FindString(ListBox1.SelectedItem.ToString())
            Console.Write(index2)

            If (index2 = -1) = False Then
                moveitemcount = 0

                ListBox2.SelectedIndex = index2
                str = ListBox4.SelectedItem.ToString()
                moveitemcount = Integer.Parse(str)
                moveitemcount = moveitemcount + 1
                ListBox4.Items(ListBox4.SelectedIndex) = moveitemcount
            Else
                ListBox2.Items.Add(ListBox1.SelectedItem.ToString())


            End If


        End If


    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        Dim index As Integer
        Dim moveitemcount As Integer
        Dim index2 As Integer

        Dim str As String
        index = 0
        moveitemcount = 0
        index2 = 0

        str = ListBox4.SelectedItem.ToString()
        moveitemcount = Integer.Parse(str)

        If moveitemcount >= 1 Then
            moveitemcount = moveitemcount - 1
            ListBox4.Items(ListBox4.SelectedIndex) = moveitemcount

            index2 = ListBox1.FindString(ListBox2.SelectedItem.ToString())
            Console.Write(index2)

            If (index2 = -1) = False Then
                moveitemcount = 0

                ListBox1.SelectedIndex = index2
                str = ListBox3.SelectedItem.ToString()
                moveitemcount = Integer.Parse(str)
                moveitemcount = moveitemcount + 1
                ListBox3.Items(ListBox3.SelectedIndex) = moveitemcount



            End If


        End If

    End Sub
End Class
