
app.service('dht', function () {
    this.sampleData = {
        "friends": [
            {"nick": "rlweb", "desc": "Hello World!"},
            {"nick": "joe", "desc": "Hello World 2"},
            {"nick": "pete", "desc": "Hello World 3"}
        ],
        "rlweb": {
            "createdOn": "2014-11-23T18:25:43.511Z",
            "nick": "rlweb",
            "desc": "Hello, welcome to my profile...."
        },
        "joe": {
            "createdOn": "2014-11-23T18:25:43.511Z",
            "nick": "joe",
            "desc": "Hello, welcome to my profile123...."
        },
        "pete": {
            "createdOn": "2014-11-23T18:25:43.511Z",
            "nick": "pete",
            "desc": "Hello, welcome to my profile321...."
        },
        "rlweb_status": [
            "10",
            "9"
        ],
        "joe_status": [
            "11"
        ],
        "pete_status": [
            "12"
        ],
        "9": {
            "nick": "rlweb",
            "createdOn": "2014-11-23T18:25:43.511Z",
            "type": "Image",
            "version": "v0.1",
            "data": {
                "imageSrc": "http://www.crosscountrytrains.co.uk/media/22701/trains_to_bristol.jpg"
            }
        }, "10": {
            "nick": "rlweb",
            "createdOn": "2014-11-23T18:25:43.511Z",
            "type": "Status",
            "version": "v0.1",
            "data": {
                "status": "Hello World!rlweb"
            }
        },
        "11": {
            "nick": "joe",
            "createdOn": "2014-11-23T18:25:43.511Z",
            "type": "Status",
            "version": "v0.1",
            "data": {
                "status": "Hello World!Joe"
            }
        }, "12": {
            "nick": "pete",
            "createdOn": "2014-11-23T18:25:43.511Z",
            "type": "Status",
            "version": "v0.1",
            "data": {
                "status": "Hello World!Pete"
            }
        }
    };
    this.get = function (key) {
        if (this.sampleData[key] === undefined) {
            return false;
        }
        return this.sampleData[key];
    };
    this.put = function (key, value) {
        this.sampleData[key] = value;
        console.log(this.sampleData);
        return true;
    };
});

app.service('yellaService', function (dht) {
    this.getUsers = function () {
        return dht.get("friends");
    };
    this.getUser = function (user) {
        return dht.get(user);
    };
    this.getUserStatusList = function (user) {
        var a = dht.get(user + "_status");
        var list = [];
        a.forEach(function (element) {
            list.push(dht.get(element));
        });
        return list;
    };
    this.newStatus = function(currentUserNick, moduleType,moduleVersion,data){
        // create binding
        var newStatus = {
            "nick": currentUserNick,
            "createdOn": new Date(),
            "type": moduleType,
            "version": moduleVersion,
            "data": data
        };
        //sha it
        var hash = CryptoJS.MD5(JSON.stringify(newStatus)).toString();
        // add status
        dht.put(hash, newStatus);
        //update status
        var getStatusList = dht.get(currentUserNick + "_status");
        getStatusList.push(hash);
        dht.put(currentUserNick + "_status", getStatusList);
        return true;
    };
    this.newUser = function (nick, desc) {
        if (this.getUser(nick)) {
            return false;
        }
        // add user
        dht.put(nick, {
            "createdOn": new Date(),
            "nick": nick,
            "desc": desc
        });
        // add users link table
        dht.put(nick + "_status", []);
        //update friends
        var friends = dht.get("friends");
        friends.push({"nick": nick, "desc": desc});
        dht.put("friends", friends);
        return true;
    };
});