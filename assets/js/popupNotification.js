function showPopupNotification(element, message, type) {
    const toast = document.createElement("div");
    toast.classList.add("toast");
    toast.innerText = message;
    element.appendChild(toast);

    switch (type) {
        case "error":
            toast.classList.add("error");
            break;
        case "warning":
            toast.classList.add("warning");
            break;
        case "success":
            toast.classList.add("success");
            break;

    }

    setTimeout(() => {
        toast.remove();
    }, 6000);
}