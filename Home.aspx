<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.Home" %>

<asp:Content ID="ContentHome" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <h3>Your work</h3>
    <div class="recentProjectWrapper">
        <div class="d-flex flex-row justify-content-between align-items-center">
            <h6>Recent projects</h6>
            <a href="ProjectPage.aspx">View all projects</a>
        </div>
        <div class="d-flex flex-row justify-content-start align-items-center">
            <asp:ListView ID="recentProjectListView" runat="server">
                <ItemTemplate>
                    <a href="TaskPage.aspx?project=<%# Eval("id") %>" style="margin-right: 1rem;">
                        <div class="task">
                            <p><%# Eval("title") %></p>
                            <div class="task__stats">
                                <span>start: <%# Eval("startDate", "{0:dd/M/yyyy}") %></span>
                                <span>lead: <%# Eval("leadName") %></span>
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <div class="irBaVa">
        <div>
            <h6>Assigned to Me</h6>
        </div>
        <div class="hEPkwz">
            <asp:ListView ID="taskAssignedListView" runat="server">
                <ItemTemplate>
                    <a href="TaskView.aspx?task=<%# Eval("id") %>" class="eHoSgc">
                        <span class="gFPPZG">
                            <span class="cCPOcx"><%# Eval("name") %></span>
                            <small class="cdaqf">
                                <span><%# Eval("projectTitle") %></span>
                                <span>-</span>
                                <span><%# showPriority(Eval("priority")) %></span>
                            </small>
                        </span>
                        <span class="kjkfvW"><%# showStatus(Eval("status")) %></span>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
