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
                        <% if ((string)Session["role"] != "INIT")
                            { %>
                        <div class="editable">
                            <a href="TaskEdit.aspx?task=<%= (long)ViewState["task"] %>" role="button" class="nch-button ml-4 hide-on-edit js-show-with-desc js-edit-desc js-edit-desc-button">Edit</a>
                        </div>
                        <% } %>
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
    <div class="window-activity">
        <div class="window-module">
            <h3>Activity</h3>
            <div class="d-flex justify-content-between align-items-center">
                <div class="d-inline-flex align-items-center">
                    <p>Show:</p>
                    <button aria-current="true" class="nch-button" data-testid="issue-activity-feed.ui.buttons.Comments" type="button">Comments</button>
                </div>
                <select id="sortSelect" class="form-control">
                    <option value="1">Newest first</option>
                    <option value="2">Older first</option>
                </select>
            </div>
            <div class="form-group">
                <textarea class="form-control status-box" id="commentTextBox" rows="3" placeholder="Enter your comment here..."></textarea>
            </div>
            <div class="button-group pull-right">
                <button type="button" id="postCommentButton" href="#" class="btn btn-primary nch-button">Post</button>
            </div>
            <div class="comment_block" id="posts"></div>
        </div>
    </div>
    <script>
        const statusDropDownList = document.getElementById("<%= statusDropDownList.ClientID %>");
        const reporterDropDownList = document.getElementById("<%= reporterDropDownList.ClientID %>");
        const assigneeDropDownList = document.getElementById("<%= assigneeDropDownList.ClientID %>");
        const QADropDownList = document.getElementById("<%= QADropDownList.ClientID %>");
        const postCommentButton = document.getElementById("postCommentButton");
        const commentTextBox = document.getElementById("commentTextBox");
        const posts = document.getElementById("posts");
        const task = <%= (long)ViewState["task"] %>;
        const sortSelect = document.getElementById("sortSelect");

        statusDropDownList.onchange = function () {
            if (statusDropDownList.value == null || statusDropDownList.selectedIndex < 1) {
                showPopupNotification(document.getElementById("toastBox"), "Invalid status", "warning");
                return;
            }
            const body = {
                task: task,
                status: statusDropDownList.value
            };
            methodPost("TaskView.aspx/updateStatus", body)
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
            if (reporterDropDownList.value == null || reporterDropDownList.selectedIndex < 1) {
                showPopupNotification(document.getElementById("toastBox"), "Invalid employee", "warning");
                return
            }
            const body = {
                task: task,
                role: "employeeReporter",
                employee: reporterDropDownList.value
            };
            methodPost("TaskView.aspx/updateEmployee", body)
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
            if (assigneeDropDownList.value == null || assigneeDropDownList.selectedIndex < 1) {
                showPopupNotification(document.getElementById("toastBox"), "Invalid employee", "warning");
                return;
            }
            const body = {
                task: task,
                role: "employeeAssignee",
                employee: assigneeDropDownList.value
            };
            methodPost("TaskView.aspx/updateEmployee", body)
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
            if (QADropDownList.value == null || QADropDownList.selectedIndex < 1) {
                showPopupNotification(document.getElementById("toastBox"), "Invalid employee", "warning");
                return;
            }
            const body = {
                task: task,
                role: "employeeQA",
                employee: QADropDownList.value
            };
            methodPost("TaskView.aspx/updateEmployee", body)
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

        postCommentButton.onclick = function () {
            if (!commentTextBox.value.trim()) {
                showPopupNotification(document.getElementById("toastBox"), "Comments not empty", "warning");
                return;
            }
            const body = {
                user: <%=(long)Session["user"] %>,
                task: task,
                name: "<%= (string)Session["name"] %>",
                content: commentTextBox.value.trim()
            };
            methodPost("TaskView.aspx/postComments", body)
                .then((result) => {
                    let response = JSON.parse(result.d);
                    if (response.error) {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "error");
                    }
                    else {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "success");
                        posts.prepend(renderComments(body.name, body.content, Date()));
                    }
                })
                .catch((err) => {
                    console.error(err);
                    showPopupNotification(document.getElementById("toastBox"), "Internal error server", "error");
                });
        }

        function renderComments(user, content, createdAt) {
            const post = document.createElement("div");
            post.classList.add("post");

            const headerPost = document.createElement("div");
            headerPost.classList.add("d-inline-flex", "flex-row", "justify-content-between", "align-items-center", "headerPost");

            const name = document.createElement("span");
            name.innerText = user;

            const createAt = document.createElement("span");
            createAt.innerText = createdAt;

            headerPost.appendChild(name);
            headerPost.appendChild(createAt);

            const bodyPost = document.createElement("div");
            bodyPost.innerText = content;
            bodyPost.classList.add("comment_body");

            post.appendChild(headerPost);
            post.appendChild(bodyPost);
            return post;
        }

        function loadComments(offset, sort, clear) {
            methodGet(`TaskView.aspx/getComments?task=${task}&offset=${offset}&order=${sort}`)
                .then((result) => {
                    if (clear) {
                        posts.innerHTML = "";
                    }
                    let response = JSON.parse(result.d);
                    response.forEach((v) => {
                        posts.append(renderComments(v.employeeName, v.content,new Date(v.createAt)));
                    });
                })
                .catch((err) => {
                    console.error(err);
                    showPopupNotification(document.getElementById("toastBox"), "Internal error server", "error");
                });
        }

        sortSelect.onchange = function () {
            if (sortSelect.value == null || sortSelect.selectedIndex < 0) {
                return;
            }
            loadComments(0, sortSelect.value, true);
        }

        loadComments(0, sortSelect.value, true);
    </script>
</asp:Content>
