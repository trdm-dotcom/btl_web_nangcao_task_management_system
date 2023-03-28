function methodGet(url) {
    console.log(`do request ajaxt method get url ${url}`);
    return new Promise(function (resolve, reject) {
        let req = new XMLHttpRequest();
        req.open("GET", url, true);
        req.setRequestHeader("content-type", "application/json");
        req.setRequestHeader("Accept", "application/json");
        req.onload = () => {
            if (req.status == 200) resolve(JSON.parse(req.responseText));
            else reject(Error(req.statusText));
        };
        req.onerror = () => {
            reject(Error('error fetching JSON data'));
        };
        req.send(null);
    });
}

function methodPost(url, body) {
    console.log(`do request ajaxt method post url ${url}`);
    return new Promise(function (resolve, reject) {
        let req = new XMLHttpRequest();
        req.open("POST", url, true);
        req.setRequestHeader("content-type", "application/json; charset=utf-8");
        req.setRequestHeader("Accept", "application/json");
        req.onload = () => {
            if (req.status == 200) resolve(JSON.parse(req.responseText));
            else reject(Error(req.statusText));
        };
        req.onerror = () => {
            reject(Error('error fetching JSON data'));
        };
        req.send(JSON.stringify(body));
    });
}

function methodPut(url, body) {
    console.log(`do request ajaxt method put url ${url}`);
    return new Promise(function (resolve, reject) {
        let req = new XMLHttpRequest();
        req.open("PUT", url, true);
        req.setRequestHeader("content-type", "application/json; charset=utf-8");
        req.setRequestHeader("Accept", "application/json");
        req.onload = () => {
            if (req.status == 200) resolve(JSON.parse(req.responseText));
            else reject(Error(req.statusText));
        };
        req.onerror = () => {
            reject(Error('error fetching JSON data'));
        };
        req.send(JSON.stringify(body));
    });
}

function methodDelete(url) {
    console.log(`do request ajaxt method delete url ${url}`);
    return new Promise(function (resolve, reject) {
        let req = new XMLHttpRequest();
        req.open("DELETE", url, true);
        req.setRequestHeader("content-type", "application/json");
        req.setRequestHeader("Accept", "application/json");
        req.onload = () => {
            if (req.status == 200) resolve(JSON.parse(req.responseText));
            else reject(Error(req.statusText));
        };
        req.onerror = () => {
            reject(Error('error fetching JSON data'));
        };
        req.send(null);
    });
}
