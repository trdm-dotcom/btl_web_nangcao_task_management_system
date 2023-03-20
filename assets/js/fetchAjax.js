function methodGet(url) {
  return new Promise(function (resolve, reject) {
    let req = window.XMLHttpRequest
      ? new XMLHttpRequest()
      : new window.ActiveXObject("Microsoft.XMLHTTP");
    req.open("GET", url, true);
    req.timeout = 20000;
    req.onload = () => {
      if (req.status == 200) resolve(JSON.parse(req.responseText));
      else if (req.status == 404) throw new Error("Not found");
    };
    req.ontimeout = (event) => {
      throw new Error("Timeout");
    };
    req.onerror = (error) => {
      reject(error);
    };
    try {
      req.send(null);
    } catch (error) {
      reject(error);
    }
  });
}

function methodPost(url, body) {
  return new Promise(function (resolve, reject) {
    let req = window.XMLHttpRequest
      ? new XMLHttpRequest()
      : new window.ActiveXObject("Microsoft.XMLHTTP");
    req.open("POST", url, true);
    req.timeout = 20000;
    req.onload = () => {
      if (req.status == 200) resolve(JSON.parse(req.responseText));
      else if (req.status == 404) throw new Error("Not found");
    };
    req.ontimeout = (event) => {
      throw new Error("Timeout");
    };
    req.onerror = (error) => {
      reject(error);
    };
    try {
      req.send(JSON.stringify(body));
    } catch (error) {
      reject(error);
    }
  });
}

function methodPut(url, body) {
  return new Promise(function (resolve, reject) {
    let req = window.XMLHttpRequest
      ? new XMLHttpRequest()
      : new window.ActiveXObject("Microsoft.XMLHTTP");
    req.open("PUT", url, true);
    req.timeout = 20000;
    req.onload = () => {
      if (req.status == 200) resolve(JSON.parse(req.responseText));
      else if (req.status == 404) throw new Error("Not found");
    };
    req.ontimeout = (event) => {
      throw new Error("Timeout");
    };
    req.onerror = (error) => {
      reject(error);
    };
    try {
      req.send(JSON.stringify(body));
    } catch (error) {
      reject(error);
    }
  });
}

function methodDelete(url) {
  return new Promise(function (resolve, reject) {
    let req = window.XMLHttpRequest
      ? new XMLHttpRequest()
      : new window.ActiveXObject("Microsoft.XMLHTTP");
    req.open("DELETE", url, true);
    req.timeout = 20000;
    req.onload = () => {
      if (req.status == 200) resolve(JSON.parse(req.responseText));
      else if (req.status == 404) throw new Error("Not found");
    };
    req.ontimeout = (event) => {
      throw new Error("Timeout");
    };
    req.onerror = (error) => {
      reject(error);
    };
    try {
      req.send(null);
    } catch (error) {
      reject(error);
    }
  });
}
