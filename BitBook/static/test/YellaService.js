describe('UserController function', function () {
    beforeEach(module('yella'));
    beforeEach(inject(function (_dht_) {
        dht = _dht_;
        spyOn(dht, 'get').and.callFake(function (key) {
            if (key == 'friends') {
                return ["milkybar", "joe", "pete", "tom"];
            }
            if (key == 'rlweb') {
                return {
                    "createdOn": "2014-11-23T18:25:43.511Z",
                    "nick": "milkybar",
                    "desc": "Milkybars are on me!"
                };
            }
            if (key == 'milkybar_status') {
                return [
                    "mfqrf8prtm",
                    "tvqtyc9qr9"
                ];
            }
            if (key == 'mfqrf8prtm') {
                return {
                    "nick": "milkybar",
                    "createdOn": "2014-05-14T13:55:44",
                    "type": "Image",
                    "version": "v0.1",
                    "data": {
                        "imageSrc": "http://www.crosscountrytrains.co.uk/media/22701/trains_to_bristol.jpg"
                    }
                };
            }
            if (key == 'tvqtyc9qr9') {
                return {
                    "nick": "milkybar",
                    "createdOn": "2014-05-14T13:55:44",
                    "type": "Status",
                    "version": "v0.1",
                    "data": {
                        "status": "Don't feel bad. It's not procrastinating, if your drinking coffee. Its Procaffeinating."
                    }
                };
            }
        });
    }));

    it('check get Users return correct numbers of users', inject(function (yellaService) {
        expect(yellaService.getUsers().length).toEqual(4);
    }));

    it('check get User return', inject(function (yellaService) {
        expect(yellaService.getUser('rlweb').createdOn).toEqual("2014-11-23T18:25:43.511Z");
        expect(yellaService.getUser('rlweb').nick).toEqual("milkybar");
        expect(yellaService.getUser('rlweb').desc).toEqual("Milkybars are on me!");
    }));

    it('check getUserStatusList', inject(function (yellaService) {
        expect(yellaService.getUserStatusList('milkybar')[0].nick).toEqual("milkybar");
        expect(yellaService.getUserStatusList('milkybar')[1].nick).toEqual("milkybar");
        expect(yellaService.getUserStatusList('milkybar')[0].type).toEqual("Image");
        expect(yellaService.getUserStatusList('milkybar')[1].type).toEqual("Status");
        expect(yellaService.getUserStatusList('milkybar')[0].data.imageSrc).toEqual("http://www.crosscountrytrains.co.uk/media/22701/trains_to_bristol.jpg");
        expect(yellaService.getUserStatusList('milkybar')[1].data.status).toEqual("Don't feel bad. It's not procrastinating, if your drinking coffee. Its Procaffeinating.");
    }));
});