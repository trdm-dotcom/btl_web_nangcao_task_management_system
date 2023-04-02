<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="TaskView.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.task.TaskView" %>

<asp:Content ID="ContentTaskView" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
   <div class="messageFeedback">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <asp:Label ID="successMessage" runat="server" CssClass="success-feedback"></asp:Label>
        <div id="toastBox"></div>
    </div>
    <div class="window-header js-card-detail-header">
        <div class="window-title">
            <asp:Label ID="nameTaskLabel" runat="server" CssClass="h2"></asp:Label>
        </div>
    </div>
    <div class="row no-gutters">
        <div class="col c-12 m-9 l-9 ">
            <div class="window-main-col">
                <div class="window-module">
                    <div class="window-module-title window-module-title-no-divider description-title">
                        <h3>Description</h3>
                        <div class="editable">
                            <a href="TaskEdit.aspx?task=<%= getId() %>" role="button" class="nch-button ml-4 hide-on-edit js-show-with-desc js-edit-desc js-edit-desc-button">Edit</a>
                        </div>
                    </div>
                    <div class="u-gutter">
                        <div class="description-content js-desc-content">
                            <asp:Panel ID="descriptionPanel" runat="server" CssClass="current markeddown hide-on-edit js-desc js-show-with-desc"></asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="col c-12 m-3 l-3 window-sidebar">
            <div class="window-sidebar">
                <div class="window-module">
                    <asp:DropDownList ID="statusDropDownList" runat="server" CssClass="form-control" Style="margin-bottom: 2rem">
                        <Items>
                            <asp:ListItem Text="-Select-" />
                            <asp:ListItem Text="TO DO" Value="TODO" />
                            <asp:ListItem Text="PROGESS" Value="PROGESS" />
                            <asp:ListItem Text="DONE" Value="DONE" />
                        </Items>
                    </asp:DropDownList>
                    <div class="border detail-box">
                        <div class="border-botton header-detail-box">
                            <p>Details</p>
                        </div>
                        <div class="main-detail-box">
                            <div class="child-element-detail-box">
                                <p>Reporter</p>
                                <asp:DropDownList ID="reporterDropDownList" runat="server" CssClass="form-control">
                                    <Items>
                                        <asp:ListItem Text="-Select-" />
                                    </Items>
                                </asp:DropDownList>
                            </div>
                            <div class="child-element-detail-box">
                                <p>Assignee</p>
                                <asp:DropDownList ID="assigneeDropDownList" runat="server" CssClass="form-control">
                                    <Items>
                                        <asp:ListItem Text="-Select-" />
                                    </Items>
                                </asp:DropDownList>
                            </div>
                            <div class="child-element-detail-box">
                                <p>QA</p>
                                <asp:DropDownList ID="QADropDownList" runat="server" CssClass="form-control">
                                    <Items>
                                        <asp:ListItem Text="-Select-" />
                                    </Items>
                                </asp:DropDownList>
                            </div>
                            <div class="child-element-detail-box">
                                <p>Priority</p>
                                <asp:Label ID="priorityLabel" runat="server"></asp:Label>
                            </div>
                            <div class="child-element-detail-box">
                                <p>Start date</p>
                                <asp:Label ID="startDateLabel" runat="server"></asp:Label>
                            </div>
                            <div class="child-element-detail-box">
                                <p>Estimate date</p>
                                <asp:Label ID="estimateDateLabel" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        const statusDropDownList = document.getElementById("<%= statusDropDownList.ClientID %>");
        const reporterDropDownList = document.getElementById("<%= reporterDropDownList.ClientID %>");
        const assigneeDropDownList = document.getElementById("<%= assigneeDropDownList.ClientID %>");
        const QADropDownList = document.getElementById("<%= QADropDownList.ClientID %>");

        statusDropDownList.onchange = function () {
            if (statusDropDownList.Value == null || statusDropDownList.selectedIndex < 1) {
                showPopupNotification(document.getElementById("toastBox"), "Invalid status", "warning");
                return;
            }
            const body = {
                taskId: <%= getId() %>,
                status: statusDropDownList.Value
            };
            methodPut("TaskView.aspx/updateStatus", body)
                .then((result) => {
                    let response = JSON.parse(result.d);
                    if (response.error) {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "error");
                    }
                    else {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "success");
                    }
                })
                .catch((err) => {
                    console.error(err);
                    showPopupNotification(document.getElementById("toastBox"), "Internal error server", "error");
                });
        }

        reporterDropDownList.onchange = function () {
            if (reporterDropDownList.Value == null || reporterDropDownList.selectedIndex < 1) {
                showPopupNotification(document.getElementById("toastBox"), "Invalid employee", "warning");
                return
            }
            const body = {
                taskId: <%= getId() %>,
                role: "employeeReporter",
                employeeId: reporterDropDownList.Value
            };
            methodPut("TaskView.aspx/updateEmployee", body)
                .then((result) => {
                    let response = JSON.parse(result.d);
                    if (response.error) {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "error");
                    }
                    else {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "success");
                    }
                })
                .catch((err) => {
                    console.error(err);
                    showPopupNotification(document.getElementById("toastBox"), "Internal error server", "error");
                });
        }

        assigneeDropDownList.onchange = function () {
            if (assigneeDropDownList.Value == null || assigneeDropDownList.selectedIndex < 1) {
                showPopupNotification(document.getElementById("toastBox"), "Invalid employee", "warning");
                return;
            }
            const body = {
                taskId: <%= getId() %>,
                role: "employeeAssignee",
                employeeId: assigneeDropDownList.Value
            };
            methodPut("TaskView.aspx/updateEmployee", body)
                .then((result) => {
                    let response = JSON.parse(result.d);
                    if (response.error) {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "error");
                    }
                    else {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "success");
                    }
                })
                .catch((err) => {
                    console.error(err);
                    showPopupNotification(document.getElementById("toastBox"), "Internal error server", "error");
                });
        }

        QADropDownList.onchange = function () {
            if (QADropDownList.Value == null || QADropDownList.selectedIndex < 1) {
                showPopupNotification(document.getElementById("toastBox"), "Invalid employee", "warning");
                return;
            }
            const body = {
                taskId: <%= getId() %>,
                role: "employeeQA",
                employeeId: QADropDownList.Value
            };
            methodPut("TaskView.aspx/updateEmployee", body)
                .then((result) => {
                    let response = JSON.parse(result.d);
                    if (response.error) {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "error");
                    }
                    else {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "success");
                    }
                })
                .catch((err) => {
                    console.error(err);
                    showPopupNotification(document.getElementById("toastBox"), "Internal error server", "error");
                });
        }
    </script>
</asp:Content>
