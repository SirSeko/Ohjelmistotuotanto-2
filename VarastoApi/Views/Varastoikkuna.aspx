<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Varastoikkuna.aspx.cs" Inherits="VarastoApi.Views.Shared.Varastoikkuna" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Varasto</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Varasto</h1>
            <hr />
         </div>
            <!--Tähän lista varastossa olevista tavaroista. Tyyli auto format-->
            <p>Varastossa olevat tavarat<asp:GridView ID="GridView_varasto" runat="server" DataSourceID="DatasourceVarasto" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField HeaderText="Nimi" ReadOnly="True" />
                    <asp:BoundField HeaderText="Koko" ReadOnly="True" />
                    <asp:BoundField HeaderText="Määrä" ReadOnly="True" />
                    <asp:BoundField HeaderText="Kommentit" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <!--TARKISTA DATASOURCE JA YHTEYS!!!-->
                <asp:SqlDataSource ID="DatasourceVarasto" runat="server"></asp:SqlDataSource>
        </p>
            <!--Testi-->

        <!--Tekstikentät uuden tavaran/materiaalin lisäykseen-->
        <p>Uusi tavara/materiaali</p>
        <p>
            <asp:TextBox ID="TextBox_uusiTavara" runat="server" ToolTip="Lisää tähän tavara/materiaali"></asp:TextBox>
            _____
            <asp:TextBox ID="TextBox_uusiMaara" runat="server" ToolTip="Lisää tähän määrä">1</asp:TextBox>
            _____
            <asp:TextBox ID="TextBox_uusiKommentti" runat="server" ToolTip="Kommenttikenttä"></asp:TextBox>
            _____
            <asp:Button ID="Button_uusiTavaraok" runat="server" Text="OK" OnClick="Button_uusiTavaraok_Click" />
            _____
           <asp:Label ID="Label_uusiTavara" runat="server" Text="Onnistui!" Visible="False"></asp:Label>
        </p>
        <hr />

            <!--Tähän lista lainoista. Tyyli auto format-->
            <p>Lainat<asp:GridView ID="GridView_lainat" runat="server" DataSourceID="DatasourceVarasto" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField HeaderText="Nimi" ReadOnly="True" />
                    <asp:BoundField HeaderText="Koko" ReadOnly="True" />
                    <asp:BoundField HeaderText="Määrä" ReadOnly="True" />
                    <asp:BoundField HeaderText="Alkupvm" ReadOnly="True" />
                    <asp:BoundField HeaderText="Loppupvm" ReadOnly="True" />
                    <asp:BoundField HeaderText="Kommentit" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
        </p>
            <!--Testi-->

         <!--Tekstikentät uuden lainan lisäykseen-->
        <p>Uusi laina</p>
        <p>
            <asp:TextBox ID="TextBox_lainaTavara" runat="server" ToolTip="Lisää tähän lainattava tavara/materiaali"></asp:TextBox>
            _____
            <asp:TextBox ID="TextBox_lainaMaara" runat="server" ToolTip="Lisää tähän määrä">1</asp:TextBox>
            _____
            <asp:TextBox ID="TextBox_lainaKommentti" runat="server" ToolTip="Kommenttikenttä"></asp:TextBox>
            _____            
            <asp:TextBox ID="TextBox_lainaAlku" runat="server" ToolTip="Lainan alkamispäivä"></asp:TextBox>
            _____
            <asp:TextBox ID="TextBox_lainaLoppu" runat="server" ToolTip="Lainan loppumispäivä"></asp:TextBox>
            _____
            <asp:Button ID="Buttonlainaok" runat="server" Text="OK" OnClick="Button_uusiLainaok_Click" />
            _____
            <asp:Label ID="Label_lainaonnistui" runat="server" Text="Onnistui!" Visible="False"></asp:Label>
        </p>
        <hr />
    </form>
</body>
</html>
