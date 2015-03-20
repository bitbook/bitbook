
app.service('dht', function () {
    this.sampleData = {
        "friends": [
            "milkybar","joe","pete","tom"
        ],
        "milkybar": {
            "createdOn": "2014-11-23T18:25:43.511Z",
            "nick": "milkybar",
            "desc": "Milkybars are on me!"
        },
        "joe": {
            "createdOn": "2014-11-23T18:25:43.511Z",
            "nick": "joe",
            "desc": "Done artfully and wisely, living dangerously engages our intellect, advances society and even makes us happier."
        },
        "pete": {
            "createdOn": "2014-11-23T18:25:43.511Z",
            "nick": "pete",
            "desc": "Hello, welcome to my profile..."
        },
        "tom": {
            "createdOn": "2014-11-23T18:25:43.511Z",
            "nick": "tom",
            "desc": "Tom, dick and harry."
        },
        "milkybar_status": [
            "mfqrf8prtm",
            "tvqtyc9qr9"
        ],
        "joe_status": [
            "44s2p06uui",
            "44s2p06123"
        ],
        "pete_status": [
            "dld6zbiyl0"
        ],
        "tom_status": [
        ],
        "mfqrf8prtm": {
            "nick": "milkybar",
            "createdOn": "2014-05-14T13:55:44",
            "type": "Image",
            "version": "v0.1",
            "data": {
                "imageSrc": "http://www.crosscountrytrains.co.uk/media/22701/trains_to_bristol.jpg"
            }
        },
        "tvqtyc9qr9": {
            "nick": "milkybar",
            "createdOn": "2014-05-14T13:55:44",
            "type": "Status",
            "version": "v0.1",
            "data": {
                "status": "Don't feel bad. It's not procrastinating, if your drinking coffee. Its Procaffeinating."
            }
        },
        "44s2p06uui": {
            "nick": "joe",
            "createdOn": "2014-06-29T13:44:43",
            "type": "Status",
            "version": "v0.1",
            "data": {
                "status": "A stranger is a friend you’ve never met before."
            }
        },
        "44s2p06123": {
            "nick": "joe",
            "createdOn": "2014-05-27T07:15:36",
            "type": "Status",
            "version": "v0.1",
            "data": {
                "status": "The 2n+1 Wheel!"
            }
        }, "dld6zbiyl0": {
            "nick": "pete",
            "createdOn": "2014-12-23T00:48:50",
            "type": "Image",
            "version": "v0.1",
            "data": {
                "imageSrc": "http://40.media.tumblr.com/9d070fe12d83f92263992a11f4b013db/tumblr_njzfe3huZ21qznjtzo1_500.jpg"
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
        return true;
    };
});

app.service('yellaService', function (dht) {
    this.getUsers = function () {
        var users = dht.get("friends");
        console.log(dht.get("friends"));
        var userList = [];
        users.forEach(function (element) {
            userList.push(dht.get(element));
        });
        return userList;
    };
    this.getUser = function (user) {
        return dht.get(user);
    };
    this.getUserStatusList = function (user) {
        var listOfStatusIds = dht.get(user + "_status");
        var listofStatus = [];
        listOfStatusIds.forEach(function (element) {
            listofStatus.push(dht.get(element));
        });
        return listofStatus;
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
        var hashofStatus = CryptoJS.MD5(JSON.stringify(newStatus)).toString();
        // add status
        dht.put(hashofStatus, newStatus);
        //update status
        var getStatusList = dht.get(currentUserNick + "_status");
        getStatusList.push(hashofStatus);
        dht.put(currentUserNick + "_status", getStatusList);
        return true;
    };
    this.newUser = function (nick, desc) {
        // check user doesn't already exist
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
        friends.push(nick);
        dht.put("friends", friends);
        return true;
    };
});