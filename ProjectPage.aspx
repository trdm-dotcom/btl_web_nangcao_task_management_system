<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectPage.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.project.ProjectPage" %>

<asp:Content ID="ContentProjectPage" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <div class="messageFeedback">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <asp:Label ID="successMessage" runat="server" CssClass="success-feedback"></asp:Label>
        <div id="toastBox"></div>
    </div>
    <div class="wrapper-table">
        <asp:GridView ID="ProjectGridView" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="ProjectGridView_PageIndexChanging" BorderStyle="None" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="TaskPage.aspx?project={0}" DataTextField="title" HeaderText="Name" />
                <asp:BoundField DataField="leadName" HeaderText="Lead" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class="badge <%# showStatus(Eval("status")) == "OPEN" ? "badge-success" : "badge-danger" %>"><%# showStatus(Eval("status")) %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="startDate" HeaderText="Start date" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="estimateDate" HeaderText="Estimate date" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <% if ((string)Session["role"] == "ADMIN")
                            { %>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("id", "ProjectEdit.aspx?project={0}") %>' Text="Edit"></asp:HyperLink>
                        <% } %>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("id", "ProjectView.aspx?project={0}") %>' Text="View"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
