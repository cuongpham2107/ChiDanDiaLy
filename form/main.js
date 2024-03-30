document.addEventListener('alpine:init', () => {
    Alpine.data('hoso', () => ({
        //alert
        index: 0,
        list: [],


        open:'',
        popup: true,
        type_registration: '',
        //Model Registration Dossier
        NameOfOrganization:'',
        FullName: '',
        Address: '',
        Phone:'',
        Email: '',
        RegistrationType:'',
        RegistrationStatus: '',
        ProductionLocations: '',
        PackagingLocations: '',
        ProjectedOutput:'',
        ProductCategories: '',
        PackageSize: '',
        ConsumptionMarket: '',
        ProductBrand: false,
        OfficeAddress: '',
        SubmissionDate: '',
        Files:[],
        //Model Registration Renew

        //Model Registration Amend
        init() {
        },
        ChooseRegistration(){
            if(this.type_registration.length !== 0){
                this.popup = false;
                this.open = this.type_registration;
            }
            else{
                this.addAlert({
                    type: "error",
                    title:"Lỗi",
                    body: "Bạn chưa chọn loại hồ sơ!!!"
                })
            }
        },
        CancelRegistration(){
            this.popup = true;
            this.open = '';
        },
        addAlert(alert) {
            this.list = [...JSON.parse(JSON.stringify(this.list)), {
                id: ++this.index,
                type: alert.type,
                title: alert.title,
                body: alert.body,
                show: false
            }]

            this.$nextTick(() => {
                this.list[this.index - 1].show = true
            })

            setTimeout(() => {
                this.list[this.index - 1].show = false
            }, 3000);
        },
        //Api
        async fetchApiRegistrationDossier(){
            await fetch(`https://localhost:44319/api/odata/Registration_Dossier`, {
                method: 'POST',
                headers: {
                    'accept':" */*",
                    'Content-Type':"application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8"
                },
                body: JSON.stringify({
                    "NameOfOrganization": "Cơ sở sản xuất chè trung du Mạnh Cường 11",
                    "FullName": "Phạm Mạnh Cường",
                    "Address": "Trường yên, Hoa lư, Ninh Bình",
                    "Phone": "0984559557",
                    "Email": "dtc18h4801020003@ictu.edu.vn",
                    "RegistrationType": "Dossier",
                    "RegistrationStatus": "SignUpSuccess",
                    "ProductionLocations": "Phường Đồng Hỷ, Thành phố Thái Nguyên, Tỉnh Thái Nguyên",
                    "PackagingLocations": "Phường Đồng Hỷ, Thành phố Thái Nguyên, Tỉnh Thái Nguyên",
                    "ProjectedOutput": "1000",
                    "ProductCategories": "Chè Trung Du",
                    "PackageSize": "Đảm bảo chất lượng",
                    "ConsumptionMarket": "Trong nước",
                    "ProductBrand": false,
                    "OfficeAddress": "Phường Đồng Hỷ, Thành phố Thái Nguyên, Tỉnh Thái Nguyên",
                    "SubmissionDate": "2024-03-26T00:00:00+07:00"
                })
            })
            .then(function (response) {
                return response.json();
            })
            .then(function (result) {
                const formData = new FormData();
                formData.append('Name',"test")
                formData.append('FileData', this.Files);
                console.log(result)
            })
            .catch (function (error) {
                console.log('Request failed', error);
            });
         
        },
        

    }))
})
