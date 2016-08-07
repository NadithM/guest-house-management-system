Imports MySql.Data.MySqlClient


Public Class Form1


    Dim cn As New MySqlConnection
    Dim cmd As MySqlCommand

    Dim flag As Integer
    Dim flagstore As Integer
    Dim flagsearch As Integer
    Dim rs As New Resizer



    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        rs.FindAllControls(Me)


        flag = 0
        flagstore = 0
        flagsearch = 0

        Panel1.Visible = False
        Panel2.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel7.Visible = False
        Panel3.Visible = False
        Panel9.Visible = False
        Button10.Visible = False
        Panel10.Visible = False
        exi_rad.Checked = True



    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ComboBox1.SelectedValue = 0
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
                Panel10.Visible = True
                Panel7.Visible = True

                gue_hou_b.Text = "DEPART"


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


                    Query = "select item from itemlist where location='" & ComboBox4.SelectedItem.ToString & "'"
                    cmd = New MySqlCommand(Query, cn)
                    reader = cmd.ExecuteReader

                    While reader.Read
                        ComboBox1.Items.Add(reader.GetString(0))
                        ComboBox3.Items.Add(reader.GetString(0))
                    End While
                    reader.Close()






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



                    sto_b.Text = "In"


                End If
                Panel1.Visible = False

                Panel2.Visible = False

                Panel4.Visible = False
                Panel3.Visible = False
                Panel10.Visible = False
                Panel7.Visible = False

                gue_hou_b.Text = "VISIT"

                DataGridView1.DataSource = vbNull
            End If




        End If





    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles sto_b.Click
        If flagstore = 0 Then
            flagstore = 1
            Panel5.Visible = True


            sto_b.Text = "Out"

            cn = New MySqlConnection
            cn.ConnectionString = "server=localhost;userid=root;database=test"
            Dim reader As MySqlDataReader
            Dim Query As String

            ListBox2.Items.Clear()
            ListBox4.Items.Clear()

            Try

                cn.Open()

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

        Else

            flagstore = 0
            Panel5.Visible = False



            sto_b.Text = "In"


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


        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Query As String
        Dim count As Integer
        count = 0


        cn = New MySqlConnection
        cn.ConnectionString = "server=localhost;userid=root;database=test"
        Dim reader As MySqlDataReader

        If ComboBox1.SelectedIndex = -1 And new_rad.Checked = False And TextBox1.Text = "" Then
            MsgBox("You Have Two Option : " & vbCrLf & " " & vbTab & "1. Select the item that has been bought from the list " & vbCrLf & " " & vbTab & "2.Type the new item that has been bought" & vbCrLf & "Then check the radio button accordingly")

        ElseIf exi_rad.Checked = True Then
            If ComboBox1.SelectedIndex = -1 Then
                MsgBox("Select the item from the list")
            Else
                If TextBox2.Text = "" Then
                    MsgBox("Enter the no of " & ComboBox1.SelectedItem.ToString & " has been bought")
                Else
                    Button10.Visible = True

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
                    Button10.Visible = True




                    Try

                        cn.Open()


                        If ComboBox4.SelectedItem.ToString = "Colombo" Then
                            Query = "INSERT INTO `storage` (`Location`, `Item`, `No_of_items`) VALUES ('Trincomalee','" & TextBox1.Text & "', '0')"
                            cmd = New MySqlCommand(Query, cn)
                            cmd.ExecuteNonQuery()


                        Else
                            Query = "INSERT INTO `storage` (`Location`, `Item`, `No_of_items`) VALUES ('Colombo','" & TextBox1.Text & "', '0')"
                            cmd = New MySqlCommand(Query, cn)
                            cmd.ExecuteNonQuery()

                        End If





                        Query = "INSERT INTO `storage` (`Location`, `Item`, `No_of_items`) VALUES ('" & ComboBox4.SelectedItem.ToString & "','" & TextBox1.Text & "', '" & TextBox2.Text & "')"
                        cmd = New MySqlCommand(Query, cn)
                        cmd.ExecuteNonQuery()






                        MsgBox("" & TextBox2.Text & " " & TextBox1.Text & " are added to the Store Room")


                        Query = "  ALTER TABLE `roomitem` ADD `" & TextBox1.Text & "` INT(4) NOT NULL DEFAULT '0'"
                        cmd = New MySqlCommand(Query, cn)
                        cmd.ExecuteNonQuery()




                        Query = "SELECT COUNT(item_id) FROM itemlist where location='" & ComboBox4.SelectedItem.ToString & "'"

                        cmd = New MySqlCommand(Query, cn)


                        reader = cmd.ExecuteReader


                        While reader.Read


                            count = reader.GetString(0)



                        End While

                        reader.Close()

                        Console.Write(count)


                        count = count + 1
                        Console.Write(count)


                        If ComboBox4.SelectedItem.ToString = "Colombo" Then
                            Query = "INSERT INTO `itemlist` (`item_id`,`item`,`location`) VALUES ('" & count & "','" & TextBox1.Text & "','Tricomalee')"
                            cmd = New MySqlCommand(Query, cn)

                            cmd.ExecuteNonQuery()


                        Else
                            Query = "INSERT INTO `itemlist` (`item_id`,`item`,`location`) VALUES ('" & count & "','" & TextBox1.Text & "','Colombo')"
                            cmd = New MySqlCommand(Query, cn)

                            cmd.ExecuteNonQuery()

                        End If

                        Query = "INSERT INTO `itemlist` (`item_id`,`item`,`location`) VALUES ('" & count & "','" & TextBox1.Text & "','" & ComboBox4.SelectedItem.ToString & "')"
                        cmd = New MySqlCommand(Query, cn)

                        cmd.ExecuteNonQuery()


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



        ComboBox1.Items.Clear()
        ComboBox3.Items.Clear()






        Try
            cn.Open()

            Query = "select item from itemlist where location='" & ComboBox4.SelectedItem.ToString & "'"
            cmd = New MySqlCommand(Query, cn)
            reader = cmd.ExecuteReader

            While reader.Read
                ComboBox1.Items.Add(reader.GetString(0))
                ComboBox3.Items.Add(reader.GetString(0))
            End While
            reader.Close()
            cn.Close()


        Catch ex As MySqlException
            MsgBox(ex.Message)
        Finally
            cn.Dispose()
        End Try





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

    Private Sub inv_man_pan_Paint(sender As Object, e As PaintEventArgs)

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

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Query As String

        If flagsearch = 0 Then
            flagsearch = 1
            Panel9.Visible = True
            Button3.Text = "Go Back"


            DataGridView1.DataSource = vbNull
            cn = New MySqlConnection
            cn.ConnectionString = "server=localhost;userid=root;database=test"
            Dim reader As MySqlDataReader


            Try

                cn.Open()


                Query = "select  room_no as `Room No.`,room_free as `Room Free`,room_full as `Room Full`," & ComboBox3.SelectedItem.ToString & " from roomitem where " & ComboBox3.SelectedItem.ToString & ">0 and location='" & ComboBox4.SelectedItem.ToString & "'"

                cmd = New MySqlCommand(Query, cn)
                Dim Adpt As New MySqlDataAdapter(Query, cn)
                Dim ds As New DataSet()
                Adpt.Fill(ds, "roomitem")
                DataGridView1.DataSource = ds.Tables(0)

                Query = "SELECT no_of_items FROM storage where location='" & ComboBox4.SelectedItem.ToString & "' and item='" & ComboBox3.SelectedItem.ToString & "'"

                cmd = New MySqlCommand(Query, cn)
                reader = cmd.ExecuteReader
                Dim count As Integer

                While reader.Read

                    count = reader.GetString(0)


                End While

                reader.Close()

                Label18.Text = "We have " & count & " " & ComboBox3.SelectedItem.ToString & "(s) in our storage" & vbCrLf & "and In rooms  we have them as in the table"


                cn.Close()



            Catch ex As MySqlException
                MsgBox(ex.Message)


            Finally
                cn.Dispose()


            End Try


        Else

            flagsearch = 0
            Panel9.Visible = False



            Button3.Text = "Search"

            Query = "select * from roomitem where location='" & ComboBox4.SelectedItem.ToString & "'"

            cmd = New MySqlCommand(Query, cn)
            Dim Adpt As New MySqlDataAdapter(Query, cn)
            Dim ds As New DataSet()
            Adpt.Fill(ds, "roomitem")
            DataGridView1.DataSource = ds.Tables(0)




        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Query As String

        Dim iter As Integer
        iter = 0

        cn = New MySqlConnection
        cn.ConnectionString = "server=localhost;userid=root;database=test"
        Dim reader As MySqlDataReader


        Try

            cn.Open()

            While iter < ListBox1.Items.Count


                Query = "update roomitem set " & ListBox1.Items(iter).ToString & "='" & ListBox3.Items(iter).ToString & "' where room_no='" & TextBox3.Text & "' and location='" & ComboBox4.SelectedItem.ToString & "'"

                Console.Write(Query)

                cmd = New MySqlCommand(Query, cn)

                cmd.ExecuteNonQuery()



                iter = iter + 1
            End While

            iter = 0

            While iter < ListBox2.Items.Count


                Query = "update storage set no_of_items='" & ListBox4.Items(iter).ToString & "' where location='" & ComboBox4.SelectedItem.ToString & "' and item='" & ListBox2.Items(iter).ToString & "'"

                Console.Write(Query)

                cmd = New MySqlCommand(Query, cn)

                cmd.ExecuteNonQuery()



                iter = iter + 1
            End While

            MsgBox("Required Item are Replaced")
            cn.Close()



        Catch ex As MySqlException
            MsgBox(ex.Message)


        Finally
            cn.Dispose()


        End Try






    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cn = New MySqlConnection
        cn.ConnectionString = "server=localhost;userid=root;database=test"
        Dim reader As MySqlDataReader
        Dim Query As String



        Try

            cn.Open()

            Query = "select room_free from roomitem where location='" & ComboBox4.SelectedItem.ToString & "' and room_no='" & TextBox3.Text & "'"
            cmd = New MySqlCommand(Query, cn)
            reader = cmd.ExecuteReader
            Dim str2 As String

            While reader.Read

                str2 = reader.GetString(0)



            End While

            reader.Close()

            If str2 = "Yes" Then

                Query = "update roomitem set  room_free ='NO', room_full='Yes' where room_no='" & TextBox3.Text & "' and location='" & ComboBox4.SelectedItem.ToString & "'"
                cmd = New MySqlCommand(Query, cn)
                cmd.ExecuteNonQuery()


                Query = "select full,empty from roomd where location='" & ComboBox4.SelectedItem.ToString & "'"
                cmd = New MySqlCommand(Query, cn)
                reader = cmd.ExecuteReader
                Dim count1 As Integer
                Dim count2 As Integer
                While reader.Read

                    count1 = reader.GetString(0)
                    count2 = reader.GetString(1)


                End While

                reader.Close()





                count1 = count1 + 1
                count2 = count2 - 1



                Query = "update roomd set  empty =" & count2 & ", full =" & count1 & " where  location='" & ComboBox4.SelectedItem.ToString & "'"
                cmd = New MySqlCommand(Query, cn)
                cmd.ExecuteNonQuery()

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


                MsgBox("Room " & TextBox3.Text & " is Booked ")
            Else
                MsgBox("Room " & TextBox3.Text & " can not be Booked ")


            End If
            cn.Close()



        Catch ex As MySqlException
            MsgBox(ex.Message)


        Finally
            cn.Dispose()


        End Try
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        cn = New MySqlConnection
        cn.ConnectionString = "server=localhost;userid=root;database=test"
        Dim reader As MySqlDataReader
        Dim Query As String



        Try

            cn.Open()

            Query = "select room_free from roomitem where location='" & ComboBox4.SelectedItem.ToString & "' and room_no='" & TextBox3.Text & "'"
            cmd = New MySqlCommand(Query, cn)
            reader = cmd.ExecuteReader
            Dim str2 As String

            While reader.Read

                str2 = reader.GetString(0)



            End While

            reader.Close()

            If str2 = "NO" Or str2 = "No" Or str2 = "no" Then

                Query = "update roomitem set  room_free ='Yes',room_full='No' where room_no='" & TextBox3.Text & "' and location='" & ComboBox4.SelectedItem.ToString & "'"

                Console.Write(Query)

                cmd = New MySqlCommand(Query, cn)

                cmd.ExecuteNonQuery()





                Query = "select full,empty from roomd where location='" & ComboBox4.SelectedItem.ToString & "'"

                cmd = New MySqlCommand(Query, cn)
                reader = cmd.ExecuteReader
                Dim count1 As Integer
                Dim count2 As Integer
                While reader.Read

                    count1 = reader.GetString(0)
                    count2 = reader.GetString(1)


                End While
                reader.Close()





                count1 = count1 - 1
                count2 = count2 + 1

                Query = "update roomd set  empty =" & count2 & ", full =" & count1 & " where  location='" & ComboBox4.SelectedItem.ToString & "'"

                cmd = New MySqlCommand(Query, cn)

                cmd.ExecuteNonQuery()







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


                MsgBox("Room " & TextBox3.Text & "is Unbooked ")


            Else


                MsgBox("Room " & TextBox3.Text & "already free ")
            End If








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
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        cn = New MySqlConnection
        cn.ConnectionString = "server=localhost;userid=root;database=test"
        Dim reader As MySqlDataReader
        Dim Query As String

        Try



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


        Button10.Visible = False
    End Sub
End Class
