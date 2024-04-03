document.addEventListener('alpine:init', () => {
    Alpine.data('hoso', () => ({
        //alert
        index: 0,
        list: [],


        open: '',
        popup: true,
        type_registration: '',

        //Model Base Registration
        NameOfOrganization: '',
        FullName: '',
        Address: '',
        Phone: '',
        Email: '',
        RegistrationType: '',
        RegistrationStatus: '',
        SubmissionDate: '',
        Files: [],
        Images: [],

        //Model Registration Dossier
        ProductionLocations: '',
        PackagingLocations: '',
        ProjectedOutput: '',
        ProductCategories: '',
        PackageSize: '',
        ConsumptionMarket: '',
        ProductBrand: false,
        OfficeAddress: '',
        
        
        //Model Registration Renew
        DeadlineToDate: '',
        DateOfIssue: '',
        CertificateNumber: '',
        NameCertificates: '',

        //Model Registration Amend
        Reason_Amend: '',
        DeadlineToDate_Amend: '',
        DateOfIssue_Amend: '',
        CertificateNumber_Amend: '',
        NameCertificates_Amend: '',
    
        init() {
        },
        ChooseRegistration() {
            if (this.type_registration.length !== 0) {
                this.popup = false;
                this.open = this.type_registration;
                //Model Base Registration
                this.NameOfOrganization= '';
                this.FullName= '';
                this.Address= '';
                this.Phone= '';
                this.Email= '';
                this.RegistrationType= '';
                this.RegistrationStatus= '';
                this.SubmissionDate= '';
                this.Files= [];
                this.Images= [];

                //Model Registration Dossier
                this.ProductionLocations= '';
                this.PackagingLocations= '';
                this.ProjectedOutput= '';
                this.ProductCategories= '';
                this.PackageSize= '';
                this.ConsumptionMarket= '';
                this.ProductBrand= false;
                this.OfficeAddress= '';

                //Model Registration Renew
                this.DeadlineToDate= '';
                this.DateOfIssue= '';
                this.CertificateNumber= '';
                this.NameCertificates= '';

                //Model Registration Amend
                this.Reason_Amend= '';
                this.DeadlineToDate_Amend= '';
                this.DateOfIssue_Amend= '';
                this.CertificateNumber_Amend= '';
                this.NameCertificates_Amend= '';
            }
            else {
                this.addAlert({
                    type: "error",
                    title: "Lỗi",
                    body: "Bạn chưa chọn loại hồ sơ!!!"
                })
            }
        },
        CancelRegistration() {
            this.popup = true;
            this.open = '';
        },
        preview(image) {
            this.Images = Array.from(image)
            const preview = document.getElementById("preview");
            while (preview.firstChild) {
                preview.removeChild(preview.firstChild);
            }
            if (image.length === 0) {
                const para = document.createElement("p");
                para.textContent = "No files currently selected for upload";
                preview.appendChild(para);
            }
            else {
                const list = document.createElement("ul");
                list.classList.add("grid");
                list.classList.add("grid-cols-5");
                list.classList.add("gap-2");
                preview.appendChild(list);
               
                for (const file of image) {
                    const listItem = document.createElement("li");
                    const placeholderImage = document.createElement("img");
                    placeholderImage.src = "default.jpg"; // Đường dẫn đến hình ảnh mặc định
                    placeholderImage.alt = "Loading...";
                    listItem.appendChild(placeholderImage);
                    if (file.type.startsWith('image/')) {
                        const image = document.createElement("img");
                        image.src = URL.createObjectURL(file);
                        image.alt = image.title = file.name;
                
                        image.classList.add("w-36");
                        image.classList.add("h-36");
                        image.classList.add("object-cover");
                        image.onload = function() {
                            listItem.removeChild(placeholderImage);
                            listItem.appendChild(image);
                        };
                
                        listItem.appendChild(image);
                    }
                    list.appendChild(listItem);
                }
            }
        },
        //Alert
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
        //Api Registration Dossier
        async fetchApiRegistrationDossier() {
            const response = await fetch(`https://localhost:44319/api/odata/Registration_Dossier`, {
                method: 'POST',
                headers: {
                    'accept': " */*",
                    'Content-Type': "application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8"
                },
                body: JSON.stringify({
                    "NameOfOrganization": this.NameOfOrganization,
                    "FullName": this.FullName,
                    "Address": this.Address,
                    "Phone": this.Phone,
                    "Email": this.Email,
                    "RegistrationType": "Dossier",
                    "RegistrationStatus": "New",
                    "ProductionLocations": this.ProductionLocations,
                    "PackagingLocations": this.PackagingLocations,
                    "ProjectedOutput": this.ProjectedOutput,
                    "ProductCategories": this.ProductCategories,
                    "PackageSize": this.PackageSize,
                    "ConsumptionMarket": this.ConsumptionMarket,
                    "ProductBrand": this.ProductBrand,
                    "OfficeAddress": this.OfficeAddress,
                    "SubmissionDate": this.getCurrentDateTime()
                })
            })
            const result = await response.json();
           
            if(this.Images &&  Array.isArray(this.Images)){
                Promise.all(this.Images.map(async (image) => {
                    await this.createMediaData(image, result.Oid);
                }))
            }
            if (this.Files && Array.isArray(this.Files)) {
                Promise.all(this.Files.map(async (item) => {
                    let oIdFileData = await this.createFileData(item)
                    fetch(`https://localhost:44319/api/odata/Attachment`, {
                        method: 'POST',
                        headers: {
                            'accept': " */*",
                            'Content-Type': "application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8"
                        },
                        body: JSON.stringify({
                            "Name": item.name.split(".")[0],
                            "FileData": oIdFileData,
                            "Registration": result.Oid
                        })
                    })
                }))
            }
            if(result !== null){
                this.NameOfOrganization = "";
                this.FullName = "";
                this.Address = "";
                this.Phone = "";
                this.Email = "";
                this.ProductionLocations = "";
                this.PackagingLocations = "";
                this.ProjectedOutput = "";
                this.ProductCategories = "";
                this.PackageSize = "";
                this.ProductBrand = "";
                this.OfficeAddress = "";
                this.Files = [];
                this.Images = [];
                this.addAlert({
                    type: "success",
                    title: "Thành công",
                    body: "Bạn đã thêm hồ sơ đăng ký thành công!!!"
                });
            }
            else {
                this.addAlert({
                    type: "error",
                    title: "Lỗi",
                    body: "Vui lòng kiểm tra lại !!!"
                })
            }
        },
        //Api Registration Renew
        async fetchApiRegistrationRenew() {
            const response = await fetch(`https://localhost:44319/api/odata/Registration_Renew`, {
                method: 'POST',
                headers: {
                    'accept': " */*",
                    'Content-Type': "application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8"
                },
                body: JSON.stringify({
                    "NameOfOrganization": this.NameOfOrganization,
                    "FullName": this.FullName,
                    "Address": this.Address,
                    "Phone": this.Phone,
                    "Email": this.Email,
                    "RegistrationType": "Renew",
                    "RegistrationStatus": "New",
                    "NameCertificates": this.NameCertificates,
                    "CertificateNumber": this.CertificateNumber,
                    "DateOfIssue": this.DateOfIssue,
                    "DeadlineToDate": this.DeadlineToDate,
                    "SubmissionDate": this.getCurrentDateTime()
                })
            })
            const result = await response.json();
           
            if(this.Images &&  Array.isArray(this.Images)){
                Promise.all(this.Images.map(async (image) => {
                    await this.createMediaData(image, result.Oid);
                }))
            }
            if (this.Files && Array.isArray(this.Files)) {
                Promise.all(this.Files.map(async (item) => {
                    let oIdFileData = await this.createFileData(item)
                    fetch(`https://localhost:44319/api/odata/Attachment`, {
                        method: 'POST',
                        headers: {
                            'accept': " */*",
                            'Content-Type': "application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8"
                        },
                        body: JSON.stringify({
                            "Name": item.name.split(".")[0],
                            "FileData": oIdFileData,
                            "Registration": result.Oid
                        })
                    })
                }))
            }
            if(result !== null){
                this.NameOfOrganization = "";
                this.FullName = "";
                this.Address = "";
                this.Phone = "";
                this.Email = "";
                this.NameCertificates= "",
                this.CertificateNumber= "",
                this.DateOfIssue= "",
                this.DeadlineToDate= "",
                this.Files = [];
                this.Images = [];
                this.addAlert({
                    type: "success",
                    title: "Thành công",
                    body: "Bạn đã thêm hồ sơ đăng ký thành công!!!"
                });
            }
            else {
                this.addAlert({
                    type: "error",
                    title: "Lỗi",
                    body: "Vui lòng kiểm tra lại !!!"
                })
            }
        },
        //Api Registratrion Amend
        async fetchApiRegistrationAmend(){
            const response = await fetch(`https://localhost:44319/api/odata/Registration_Amend`, {
                method: 'POST',
                headers: {
                    'accept': " */*",
                    'Content-Type': "application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8"
                },
                body: JSON.stringify({
                    "NameOfOrganization": this.NameOfOrganization,
                    "FullName": this.FullName,
                    "Address": this.Address,
                    "Phone": this.Phone,
                    "Email": this.Email,
                    "RegistrationType": "Amend",
                    "RegistrationStatus": "New",
                    "NameCertificates": this.NameCertificates_Amend,
                    "CertificateNumber": this.CertificateNumber_Amend,
                    "DateOfIssue": this.DateOfIssue_Amend,
                    "DeadlineToDate": this.DeadlineToDate_Amend,
                    "Reason": this.Reason_Amend,
                    "SubmissionDate": this.getCurrentDateTime()
                })
            })
            const result = await response.json();
           
            if(this.Images &&  Array.isArray(this.Images)){
                Promise.all(this.Images.map(async (image) => {
                    await this.createMediaData(image, result.Oid);
                }))
            }
            if (this.Files && Array.isArray(this.Files)) {
                Promise.all(this.Files.map(async (item) => {
                    let oIdFileData = await this.createFileData(item)
                    fetch(`https://localhost:44319/api/odata/Attachment`, {
                        method: 'POST',
                        headers: {
                            'accept': " */*",
                            'Content-Type': "application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8"
                        },
                        body: JSON.stringify({
                            "Name": item.name.split(".")[0],
                            "FileData": oIdFileData,
                            "Registration": result.Oid
                        })
                    })
                }))
            }
            if(result !== null){
                this.NameOfOrganization = "";
                this.FullName = "";
                this.Address = "";
                this.Phone = "";
                this.Email = "";
                this.NameCertificates_Amend= "",
                this.CertificateNumber_Amend= "",
                this.DateOfIssue_Amend= "",
                this.DeadlineToDate_Amend= "",
                this.Files = [];
                this.Images = [];
                this.addAlert({
                    type: "success",
                    title: "Thành công",
                    body: "Bạn đã thêm hồ sơ đăng ký thành công!!!"
                });
            }
            else {
                this.addAlert({
                    type: "error",
                    title: "Lỗi",
                    body: "Vui lòng kiểm tra lại !!!"
                })
            }
        },
        async createMediaData(file, registration_id){
            return new Promise((resolve, reject) => {
                this.fileToBase64(file, async (base64String) => {
                    let response = await fetch(`https://localhost:44319/api/odata/Media`, {
                        method: 'POST',
                        headers: {
                            'accept': " */*",
                            'Content-Type': "application/json; odata.metadata=minimal; odata.streaming=true"
                        },
                        body: JSON.stringify({
                            "Name": file.name,
                            "Photo": base64String,
                            "Registration": registration_id
                        })
                    })
                    let result = await response.json();
                    resolve(result.Oid);
                })
            })
        },
        async createFileData(file) {
            return new Promise((resolve, reject) => {
                this.fileToBase64(file, async (base64String) => {
                    let response = await fetch(`https://localhost:44319/api/odata/FileData`, {
                        method: 'POST',
                        headers: {
                            'accept': " */*",
                            'Content-Type': "application/json; odata.metadata=minimal; odata.streaming=true"
                        },
                        body: JSON.stringify({
                            "FileName": file.name,
                            "Content": base64String
                        })
                    });
                    let result = await response.json();
                    resolve(result.Oid);
                });
            });
        },
        fileToBase64(file, callback) {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = function () {
                const base64String = reader.result.split(',')[1];
                callback(base64String);
            };
            reader.onerror = function (error) {
                console.error('Error occurred while converting file to base64:', error);
            };
        },
        getCurrentDateTime() {
            const now = new Date();
            const year = now.getFullYear();
            const month = String(now.getMonth() + 1).padStart(2, '0');
            const day = String(now.getDate()).padStart(2, '0');
            const hours = String(now.getHours()).padStart(2, '0');
            const minutes = String(now.getMinutes()).padStart(2, '0');
            const seconds = String(now.getSeconds()).padStart(2, '0');
            const timeZoneOffset = -now.getTimezoneOffset() / 60; // Offset in hours, convert to positive
            const timeZoneOffsetString = `${timeZoneOffset >= 0 ? '+' : '-'}${String(Math.abs(timeZoneOffset)).padStart(2, '0')}:00`;

            return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}${timeZoneOffsetString}`;
        }
    }))
})
