var $validatorform;

function validateSingleForm(form) {
    $.validator.addMethod(
        "specialcharactor",
        function (value, element, param) {
            return value.match(/^[0-9A-Za-z\_\ \.\@]*$/);
        },
        "Special character is not allowed"
    );
    $.validator.addMethod(
        "logincharactor",
        function (value, element, param) {
            return value.match(/^[0-9A-Za-z\#\@\$\!\%]*$/);
        },
        "Special character is not allowed"
    );
    $.validator.addMethod(
        "passwordmatch",
        function (value, element, param) {
            return value.match(/^((?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{8,})$/);
        },
        "Please enter Password at least one digit/lowercase/uppercase letter and be at least 8 characters long"
    );
    //$.validator.addMethod(
    //    "Ipaddress1",
    //    function (value, element, param) {
    //        return value.match(/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/);
    //    },
    //    "Invalid IP Address format"
    //);
    $.validator.addMethod(
        "Ipaddress",
        function (value, element, param) {
            return value.match(/^[0-9\.]*$/);
        },
        "Invalid IP Address format"
    );
    $.validator.addMethod(
        "Path",
        function (value, element, param) {
            //return value.match(/^[0-9A-Za-z\_\\\/\.\-\ \:]*$/);
            //return value.match(/^[a-z]:(\/|\\\\)([a-zA-Z0-9_\-]+\\1)*[a-zA-Z0-9_ @\-]+/);
            return value.match(/^([A-Za-z]:)(\\[A-Za-z_\-\s0-9\.]+)+$/);
        },
        "Please enter valid path"
    );
    $.validator.addMethod(
        "emailvalid",
        function (value, element, param) {
            return value.match(/^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/);
        },
        "Please enter valid email"
    );
    $.validator.addMethod(
        "insuranceinfo",
        function (value, element, param) {
            return value.match(/^[0-9\-]*$/);
        },
        "Invalid date Fromat"
    );
    $.validator.addMethod(
        "Offlinesysdata",
        function (value, element, param) {
            return value.match(/^[0-9A-Za-z\.\-\(\)\ \/]*$/);
        },
        "Invalid format"
    );
    $validatorform = jQuery(form).validate({
        rules: {
            //login page
            username: {
                required: true,
                logincharactor:true
            },
            password: {
                required: true,
                logincharactor: true,
                minlength: 8,
                maxlength:20
            },
            txtInput: {
                required: true,
                specialcharactor: true
            },
            //Forgot Password
            user_name: {
                required: true,
                specialcharactor: true
            },
            securityquestion: {
                required: true
            },
            securityanswer: {
                required: true,
                specialcharactor: true
            },
            //Master Password
            masterpassword: {
                required: true,
                specialcharactor: true
            },
            //Home (Dashboard)
            ip: {
                Ipaddress: true
            },
            ip_policy: {
                Ipaddress: true
            },
            dlip: {
                Ipaddress: true
            },
            ip_linkage: {
                Ipaddress: true
            },
            ip_count: {
                Ipaddress: true
            },
            ip_audit: {
                Ipaddress: true
            },
            softip: {
                Ipaddress: true
            },
            userip: {
                Ipaddress: true
            },
            //asset_system_list
            lblsendmsg: {
                required: true,
                specialcharactor: true
            },
            lblcommand: {
                required: true,
                specialcharactor: true
            },
            action: {
                required: true
            },
            data: {
                required: true
            },
            txt_uninstall_old_password: {
                required: true,
                logincharactor: true
            },
            txt_uninstall_new_password: {
                required: true,
                logincharactor: true
            },
            txt_uninstall_confirm_password: {
                required: true,
                logincharactor: true,
                equalTo: "#txt_uninstall_new_password"
            },
            pathset: {
                required: true,
                Path: true
            },
            keyset: {
                required: true,
                specialcharactor: true
            },
            keytype: {
                required: true
            },
            keyaction: {
                required: true
            },
            key_valueset: {
                required: true,
                specialcharactor: true
            },
            intranetip1: {
                required: true,
                Ipaddress: true
            },
            intranetip2: {
                Ipaddress: true
            },
            internetip1: {
                Ipaddress: true
            },
            internetip2: {
                Ipaddress: true
            },
            drpip: {
                Ipaddress: true
            },
            //Asset_system_detail
            txtdname: {
                specialcharactor: true
            },
            txtemail: {
                specialcharactor: true
            },
            //offline system info
            Motherboard: {
                specialcharactor: true
            },
            Processor: {
                Offlinesysdata: true
            },
            Manufectural: {
                Offlinesysdata: true
            },
            Model: {
                specialcharactor: true
            },
            OSName: {
                specialcharactor: true
            },
            HDDList: {
                specialcharactor: true
            },
            RAMType: {
                specialcharactor: true
            },
            HDDSize: {
                specialcharactor: true
            },
            RAMSlots: {
                Offlinesysdata: true,
                digits: true
            },
            HDDType: {
                specialcharactor: true
            },
            RAMSize: {
                specialcharactor: true
            },
            Keyboard: {
                Offlinesysdata: true
            },
            Mouse: {
                Offlinesysdata: true
            },
            FloppyDrive: {
                specialcharactor: true
            },
            CDROM: {
                specialcharactor: true
            },
            NICCard: {
                Offlinesysdata: true
            },
            Monitor: {
                specialcharactor: true
            },
            //frm_Purchase_info this is from id
            PCost: {
                specialcharactor: true,
                digits: true,
                Ipaddress: true
            },
            Porder: {
                specialcharactor: true,
                digits: true
            },
            PInvoice: {
                specialcharactor: true,
                digits: true
            },
            txtattachment: {
                specialcharactor: true
            },
            Remark: {
                specialcharactor: true
            },
            //frm_AMC_info this is from id 
            Warrantyperiodform: {
                date: true
            },
            Warrantyperiodto: {
                date: true
            },
            AMCfrom: {
                date: true
            },
            AMCto: {
                date: true
            },
            VendorName: {
                required: true,
                specialcharactor: true
            },
            vendor_check: {
                required: true,
                specialcharactor: true
            },
            AMCcost: {
                specialcharactor: true
            },
            VenderLocation: {
                specialcharactor: true
            },
            //frm_Insurance_info this is id from 
            Insuranceparty: {
                specialcharactor: true
            },
            InsurancepartyName: {
                specialcharactor: true
            },
            Insurancefrom: {
                insuranceinfo: true
            },
            Insuranceto: {
                insuranceinfo: true
            },
            InsuranceAmount: {
                specialcharactor: true,
                digits: true
            },
            //Hardware Master
            HardwareName: {
                required: true
            },
            txtHardwareName: {
                required: true,
                specialcharactor: true
            },
            Manufacture: {
                specialcharactor: true
            },
            ModelNo: {
                specialcharactor: true
            },
            txtVendorName: {
                specialcharactor: true
            },
            txtamcVendorName: {
                specialcharactor: true
            },
            PONo: {
                specialcharactor: true
            },
            InvoiceNo: {
                specialcharactor: true
            },
            AssetID: {
                specialcharactor: true
            },
            MachineSerialNo: {
                specialcharactor: true
            },
            //edit hardware master
            drpHardwareName: {
                required: true
            },
            editHardwareName: {
                required: true,
                specialcharactor: true
            },
            editManufacture: {
                specialcharactor: true
            },
            editModelNo: {
                specialcharactor: true
            },
            editVendorName: {
                specialcharactor: true
            },
            edittxtamcVendorName: {
                specialcharactor: true
            },
            editPONo: {
                specialcharactor: true
            },
            editInvoiceNo: {
                specialcharactor: true
            },
            editAssetID: {
                specialcharactor: true
            },
            editRemark: {
                specialcharactor: true
            },
            editMachineSerialNo: {
                specialcharactor: true
            },
            //Software Master
            txtsoftName: {
                required: true,
                specialcharactor: true
            },
            proddesc: {
                specialcharactor: true
            },
            txtkey: {
                specialcharactor: true
            },
            txtvendorname: {
                specialcharactor: true
            },
            pono: {
                specialcharactor: true
            },
            nooflicense: {
                specialcharactor: true
            },
            invoicenumber: {
                specialcharactor: true
            },
            invoiceamount: {
                digits: true
            },
            editSoftwareName: {
                required: true,
                specialcharactor: true
            },
            software_check1: {
                required: true
            },
            editprodesc: {
                specialcharactor: true
            },
            editkey: {
                specialcharactor: true
            },
            editvendorname: {
                specialcharactor: true
            },
            editpo: {
                specialcharactor: true
            },
            editlicenseno: {
                specialcharactor: true
            },
            editinvoicenumber: {
                specialcharactor: true
            },
            editinvoiceamount: {
                digits: true
            },
            //Vendor master
            vendorName: {
                required: true,
                specialcharactor: true
            },
            SupplierName: {
                specialcharactor: true
            },
            Address: {
                specialcharactor: true
            },
            City: {
                specialcharactor: true
            },
            ContactPerson: {
                specialcharactor: true
            },
            EmailAddress: {
                specialcharactor: true
            },
            FaxNo: {
                specialcharactor: true
            },
            PinNo: {
                specialcharactor: true
            },
            Phone1: {
                specialcharactor: true
            },
            Phone2: {
                specialcharactor: true
            },
            Mobile1: {
                specialcharactor: true
            },
            Mobile2: {
                specialcharactor: true
            },
            GST: {
                specialcharactor: true
            },
            EvendorName: {
                required: true,
                specialcharactor: true
            },
            ESupplierName: {
                specialcharactor: true
            },
            EAddress: {
                specialcharactor: true
            },
            ECity: {
                specialcharactor: true
            },
            EContactPerson: {
                specialcharactor: true
            },
            EEmailAddress: {
                specialcharactor: true
            },
            EFaxNo: {
                specialcharactor: true
            },
            EPinNo: {
                specialcharactor: true
            },
            EPhone1: {
                specialcharactor: true
            },
            EPhone2: {
                specialcharactor: true
            },
            EMobile1: {
                specialcharactor: true
            },
            EMobile2: {
                specialcharactor: true
            },
            EGST: {
                specialcharactor: true
            },
            //Policy
            txtpolicyname: {
                required: true,
                specialcharactor: true
            },
            txtpolicydesc: {
                specialcharactor: true
            },
            txtrulesname: {      
                required: true,
                specialcharactor: true
            },
            txtdescription: {
                specialcharactor: true
            },
            txtcpuusage: {
                required: true,
                specialcharactor: true
            },
            txtmemoryusage: {
                required: true,
                specialcharactor: true
            },
            txtdiskusage: {
                required: true,
                specialcharactor: true
            },
            txtcputhresold: {
                required: true,
                specialcharactor: true
            },
            txtmemorythresold: {
                required: true,
                specialcharactor: true
            },
            drp_process: {
                required: true
            },
            process_name: { 
                required: true
            },
            share_delete_days: {
                required: true
            },
            userrulesdayalert: {
                required: true
            },
            drploginuser: {
                required: true
            },
            txt_event_id_new: {
                required: true,
                specialcharactor: true
            },
            txtfwsourceport: {
                digits: true
            },
            txtfwdestinationip: {
                required: true
            },
            //NAC policy
            nacpolicyname: {
                required: true,
                specialcharactor: true
            },
            nacip: {
                required: true,
                Ipaddress: true
            },
            nacpolicydesc: {
                specialcharactor: true
            },
            //Report  ==  agent information
            //Report  ==  modem usage
            searchvalue: {
                specialcharactor: true
            }, 
            serchvalue: {
                specialcharactor: true
            },
            //reports ==  data leakage=> system data model
            IP: {
                Ipaddress: true
            },
            //reports ==  data leakage=> raw data model
            IP1: {
                Ipaddress: true
            },
            serachvalue: {
                specialcharactor: true
            },
            //reports ==  data leakage=> reovable data usage model
            IP3: {
                Ipaddress: true
            },
            searchvalue3: {
                specialcharactor: true
            },
            //reports ==  data leakage=> printer usage model
            IP4: {
                Ipaddress: true
            },
            searchvalue4: {
                specialcharactor: true
            },
            //reports ==  data leakage=> printer usage model
            //reports ==  audit trail
            searchvalue1: {
                specialcharactor: true
            },
            searchvalue2: {
                specialcharactor: true
            },
            ip2: {
                Ipaddress: true
            },
            //reports  event monitoring
            txtsearcheventid: {
                digits: true
            },
            txtsearch: {
                specialcharactor: true
            },
            //report firewall status report
            ip1: {
                Ipaddress: true
            },
            //report software information
            txtsearchsoft: {
                specialcharactor: true
            },
            IP2: {
                Ipaddress: true
            },
            serachvalue2: {
                specialcharactor: true
            },
            serachsoft: {
                specialcharactor: true
            },
            searchsysgenrpt: {
                specialcharactor: true
            },
            txtproductserch: {
                specialcharactor: true
            },
            drpkeyIP: {
                Ipaddress: true
            },
            drpipcnt: {
                Ipaddress: true
            },
            txtserchvalue: {
                specialcharactor: true
            },
            sdvalue: {
                specialcharactor: true
            },
            drpkip: {
                Ipaddress: true
            },
            drpgenerateip: {
                Ipaddress: true
            },
            txtgeneratesearach: {
                specialcharactor: true
            },
            Svalue: {
                specialcharactor: true
            },
            drpip1: {
                Ipaddress: true
            },
            Svalue1: {
                specialcharactor: true
            },
            drpip2: {
                Ipaddress: true
            },
            Svalue2: {
                specialcharactor: true
            },
            //report port information
            portnumber: {
                required: true,
                digits: true
            },
            portname: {
                required: true,
                specialcharactor: true
            },
            //Report Network statistics
            drppip: {
                Ipaddress: true
            },
            //report enumerate files 
            drpIP: {
                Ipaddress: true
            },
            Addnewextention: {
                specialcharactor: true
            },
            //report policy apply
            drpip11: {
                Ipaddress: true
            },
            //setting_organization_structure
            txtlevelname: {
                required: true,
                specialcharactor: true
            },
            etxtleave: {
                required: true,
                specialcharactor: true
            },
            txt_ouname_add_branch_unit: {
                required: true,
                specialcharactor: true
            },
            editbranch1: {
                required: true,
                specialcharactor: true
            },
            drpUip: {
                Ipaddress: true
            },
            //setting_user_mgmt
            group_name: {
                required: true,
                specialcharactor: true
            },
            description: {
                specialcharactor: true
            },
            grouptype: {
                required: true
            },
            usertype: {
                required: true
            },
            groupname: {
                required: true,
                specialcharactor: true
            },
            emailid: {
                required: true,
                emailvalid: true
            },
            userpassword: {
                required: true,
                passwordmatch: true,
                minlength: 8,
                maxlength: 20
            },
            contactno: {
                required: true,
                number: true,
                minlength: 10,
                maxlength: 10
            },
            user_nameedit: {
                required: true,
                specialcharactor: true
            },
            usertypeedit: {
                required: true
            },
            groupnameedit: {
                required: true
            },
            emailidedit: {
                required: true,
                emailvalid: true
            },
            userpasswordedit: {
                required: true,
                passwordmatch: true,
                minlength: 8,
                maxlength: 20
            },
            contactnoedit: {
                required: true,
                number: true,
                minlength: 10,
                maxlength: 10
            },
            securityquestion1: {
                required: true
            },
            securityanswer1: {
                required: true,
                specialcharactor: true
            },
            oldpassword: {
                required: true,
                passwordmatch: true,
                minlength: 8,
                maxlength: 20
            },
            newpassword: {
                required: true,
                passwordmatch: true,
                minlength: 8,
                maxlength: 20
            },
            confirmpassword: {
                required: true,
                equalTo: "#newpassword"
            },
            //settings
            tempname: {
                required: true,
                specialcharactor: true
            },
            edittempname: {
                required: true,
                specialcharactor: true
            },
            txtemailto: {
                email: true
            },
            txtemailcc: {
                email: true
            },
            txteditemailto: {
                email: true
            },
            txteditemailcc: {
                email: true
            },
            txt_task_name: {
                required: true,
                specialcharactor: true
            },
            txt_task_desc: {
                specialcharactor: true
            },
            txt_activity_file_32: {
                required: true,
                specialcharactor: true
            },
            txt_activity_destination_path_32: {
                required: true,
                Path: true
            },
            txt_activity_file_64: {
                required: true,
                specialcharactor: true
            },
            txt_activity_destination_path_64: {
                required: true,
                Path: true
            },
            txt_activity_command_32: {
                required: true,
                specialcharactor: true
            },
            txt_activity_command_64: {
                required: true,
                specialcharactor: true
            },
            txt_activity_working_directory: {
                Path: true
            },
            txt_source_folder_path: {
                required: true,
                Path: true
            },
            txt_source_filename: {
                required: true,
                specialcharactor: true
            },
            txt_server_folder_path: {
                specialcharactor: true
            },
            txt_server_filename: {
                required: true,
                specialcharactor: true
            },
            txtsearchSW: {
                specialcharactor: true
            },
            drpip15: {
                Ipaddress: true
            },
            drpsname15: {
                specialcharactor: true
            },
            drpdname15: {
                specialcharactor: true
            },
            txtServerIP: {
                required: true,
                Ipaddress: true
            },
            txtFolder: {
                required: true,
                Path: true
            },
            //flag on off 
            filenamelist: {
                required: true
            },
            sectionlist: {
                required: true
            },
            propertylist: {
                required: true
            },
            flag_action: {
                required: true
            },
            drpou: {
                required: true
            },
            drpou_policy: {
                required: true
            },
            flagvalues: {
                required: true                
            },
            txtattachment_path: {
                required: true,
                specialcharactor: true
            },
            txtvname: {
                specialcharactor: true
            }
        },
        messages: {
            //login page
            username: {
                required: "Please enter username",
                logincharactor: "Invalid character"
            },
            password: {
                required: "Please enter passsword",
                logincharactor: "Invalid character",
                minlength: "Please enter minimum 8 character",
                maxlength: "Please enter maximum 20 character"
            }, 
            txtInput: {
                required: "Please Enter valid captcha",
                specialcharactor: "Invalid character"
            },
            //Forgot Password
            user_name: {
                required: "Please enter username",
                specialcharactor: "Invalid character"
            },
            securityquestion: {
                required: "Please select security question"
            },
            securityanswer: {
                required: "Please enter security answer",
                specialcharactor: "Invalid character"
            },
            //Master Password
            masterpassword: {
                required: "Please enter master password",
                specialcharactor: "Invalid character"
            },
            //Home (Dashboard)
            ip: {
                Ipaddress: "Invalid IP Address format"
            },
            ip_policy: {
                Ipaddress: "Invalid IP Address format"
            },
            dlip: {
                Ipaddress: "Invalid IP Address format"
            },
            ip_linkage: {
                Ipaddress: "Invalid IP Address format"
            },
            ip_count: {
                Ipaddress: "Invalid IP Address format"
            },
            ip_audit: {
                Ipaddress: "Invalid IP Address format"
            },
            softip: {
                Ipaddress: "Invalid IP Address format"
            },
            userip: {
                Ipaddress: "Invalid IP Address format"
            },
            //asset_system_list
            lblsendmsg: {
                required: "Please Enter Message",
                specialcharactor: "Invalid character",
            },
            lblcommand: {
                required: "Please Enter command",
                specialcharactor: "Invalid character",
            },
            action: {
                required: "You must choose an option",
            },
            data: {
                required: "You must choose an option",
            },
            txt_uninstall_old_password: {
                required: "Please enter old passsword",
                logincharactor: "Invalid character"
            },
            txt_uninstall_new_password: {
                required: "Please enter passsword",
                logincharactor: "Invalid character"
            },
            txt_uninstall_confirm_password: {
                logincharactor: "Invalid character",
                required: "Please enter sam password as a confirm password",
                equalTo: "Password does not match"
            },
            pathset: {
                required: "Please Enter Message",
                Path: "Please enter valid path"
            },
            keyset: {
                required: "Please Enter Message",
                specialcharactor: "Invalid character",
            },
            keytype: {
                required: "You must choose an option",
            },
            keyaction: {
                required: "You must choose an option",
            },
            key_valueset: {
                required: "Please Enter Message",
                specialcharactor: "Invalid character",
            },
            intranetip1: {
                required: "Please Enter IP Address",
                Ipdotaddress: "Invalid IP Address format",
            },
            intranetip2: {
                Ipdotaddress: "Invalid IP Address format",
            },
            internetip1: {
                Ipdotaddress: "Invalid IP Address format",
            },
            internetip2: {
                Ipdotaddress: "Invalid IP Address format",
            },
            drpip: {
                Ipaddress: "Invalid Ip address"
            },
            //Asset_system_detail
            txtdname: {
                specialcharactor: "Invalid character"
            },
            txtemail: {
                specialcharactor: "Invalid character"
            },
            //offline system info
            Motherboard: {
                specialcharactor: "Invalid character"
            },
            Processor: {
                Offlinesysdata: "Invalid format"
            },
            Manufectural: {
                Offlinesysdata: "Invalid format"
            },
            Model: {
                specialcharactor: "Invalid character"
            },
            OSName: {
                specialcharactor: "Invalid character"
            },
            HDDList: {
                specialcharactor: "Invalid character"
            },
            RAMType: {
                specialcharactor: "Invalid character"
            },
            HDDSize: {
                specialcharactor: "Invalid character"
            },
            RAMSlots: {
                Offlinesysdata: "Invalid format",
                digits: "Please enter digits only"
            },
            HDDType: {
                specialcharactor: "Invalid character"
            },
            RAMSize: {
                specialcharactor: "Invalid character"
            },
            Keyboard: {
                Offlinesysdata: "Invalid format"
            },
            Mouse: {
                Offlinesysdata: "Invalid format"
            },
            FloppyDrive: {
                specialcharactor: "Invalid character"
            },
            CDROM: {
                specialcharactor: "Invalid character"
            },
            NICCard: {
                Offlinesysdata: "Invalid format"
            },
            Monitor: {
                specialcharactor: "Invalid character"
            },
            //frm_Purchase_info this is from id
            PCost: {
                specialcharactor: "Invalid character",
                digits: "Enter only Digits"
            },
            Porder: {
                specialcharactor: "Invalid character",
                digits: "Enter only Digits"
            },
            PInvoice: {
                specialcharactor: "Invalid character",
                digits: "Enter only Digits"
            },
            txtattachment: {
                specialcharactor: "Invalid character"
            },
            Remark: {
                specialcharactor: "Invalid character"
            },
            //frm_AMC_info this is from id 
            Warrantyperiodform: {
                date: "Invalid date Fromat. Please enter date in this format dd/mm/yyyy "
            },
            Warrantyperiodto: {
                date: "Invalid date Fromat. Please enter date in this format dd/mm/yyyy "
            },
            AMCfrom: {
                date: "Invalid date Fromat. Please enter date in this format dd/mm/yyyy "
            },
            AMCto: {
                date: "Invalid date Fromat. Please enter date in this format dd/mm/yyyy "
            },
            VendorName: {
                required: "You must choose an option",
                specialcharactor: "Invalid character"
            },
            vendor_check: {
                required: "You must choose an option",
                specialcharactor: "Invalid character"
            },
            AMCcost: {
                specialcharactor: "Invalid character",
            },
            VenderLocation: {
                specialcharactor: "Invalid character",
            },
            //frm_Insurance_info this is id from 
            Insuranceparty: {
                specialcharactor: "Invalid character",
            },
            InsurancepartyName: {
                specialcharactor: "Invalid character",
            },
            Insurancefrom: {
                insuranceinfo: "Invalid date Fromat"
            },
            Insuranceto: {
                insuranceinfo: "Invalid date Fromat"
            },
            InsuranceAmount: {
                specialcharactor: "Invalid character",
                digits: "Enter only Digits"
            },  
            //hardware master
            HardwareName: {
                required: "Please select hardwarename",
            },
            txtHardwareName: {
                required: "Please enter hardwarename",
                specialcharactor: "Invalid character"
            },
            Manufacture: {
                specialcharactor: "Invalid character"
            },
            ModelNo: {
                specialcharactor: "Invalid character"
            },
            txtVendorName: {
                specialcharactor: "Invalid character"
            },
            txtamcVendorName: {
                specialcharactor: "Invalid character"
            },
            PONo: {
                specialcharactor: "Invalid character"
            },
            InvoiceNo: {
                specialcharactor: "Invalid character"
            },
            AssetID: {
                specialcharactor: "Invalid character"
            },
            MachineSerialNo: {
                specialcharactor: "Invalid character"
            },
            //edit hardware master
            drpHardwareName: {
                required: "Please select hardwarename",
            },
            editHardwareName: {
                required: "Please enter hardwarename",
                specialcharactor: "Invalid character"
            },
            editManufacture: {
                specialcharactor: "Invalid character"
            },
            editModelNo: {
                specialcharactor: "Invalid character"
            },
            editVendorName: {
                specialcharactor: "Invalid character"
            },
            edittxtamcVendorName: {
                specialcharactor: "Invalid character"
            },
            editPONo: {
                specialcharactor: "Invalid character"
            },
            editInvoiceNo: {
                specialcharactor: "Invalid character"
            },
            editAssetID: {
                specialcharactor: "Invalid character"
            },
            editRemark: {
                specialcharactor: "Invalid character"
            },
            editMachineSerialNo: {
                specialcharactor: "Invalid character"
            },
            //Software Master
            txtsoftName: {
                required: "Please enter software name",
                specialcharactor: "Invalid character"
            },
            proddesc: {
                specialcharactor: "Invalid character"
            },
            txtkey: {
                specialcharactor: "Invalid character"
            },
            txtvendorname: {
                specialcharactor: "Invalid character"
            },
            pono: {
                specialcharactor: "Invalid character"
            },
            nooflicense: {
                specialcharactor: "Invalid character"
            },
            invoicenumber: {
                specialcharactor: "Invalid character"
            },
            invoiceamount: {
                digits: "Please Valid Digits"
            },
            editSoftwareName: {
                required: "Please Enter Vendor Name",
                specialcharactor: "Invalid character"
            },
            software_check1: {
                required: "Please Enter Vendor Name"
            },
            editprodesc: {
                specialcharactor: "Invalid character"
            },
            editkey: {
                specialcharactor: "Invalid character"
            },
            editvendorname: {
                specialcharactor: "Invalid character"
            },
            editpo: {
                specialcharactor: "Invalid character"
            },
            editlicenseno: {
                specialcharactor: "Invalid character"
            },
            editinvoicenumber: {
                specialcharactor: "Invalid character"
            },
            editinvoiceamount: {
                digits: "Please Valid Digits"
            },
            //Vendor master
            vendorName: {
                required: "Please Enter Vendor Name",
                specialcharactor: "Invalid character"
            },
            SupplierName: {
                specialcharactor: "Invalid character"
            },
            Address: {
                specialcharactor: "Invalid character"
            },
            City: {
                specialcharactor: "Invalid character"
            },
            ContactPerson: {
                specialcharactor: "Invalid character"
            },
            EmailAddress: {
                specialcharactor: "Invalid character"
            },
            FaxNo: {
                specialcharactor: "Invalid character"
            },
            PinNo: {
                specialcharactor: "Invalid character"
            },
            Phone1: {
                specialcharactor: "Invalid character"
            },
            Phone2: {
                specialcharactor: "Invalid character"
            },
            Mobile1: {
                specialcharactor: "Invalid character"
            },
            Mobile2: {
                specialcharactor: "Invalid character"
            },
            GST: {
                specialcharactor: "Invalid character"
            },
            EvendorName: {
                required: "Please enter vendor name",
                specialcharactor: "Invalid character"
            },
            ESupplierName: {
                specialcharactor: "Invalid character"
            },
            EAddress: {
                specialcharactor: "Invalid character"
            },
            ECity: {
                specialcharactor: "Invalid character"
            },
            EContactPerson: {
                specialcharactor: "Invalid character"
            },
            EEmailAddress: {
                specialcharactor: "Invalid character"
            },
            EFaxNo: {
                specialcharactor: "Invalid character"
            },
            EPinNo: {
                specialcharactor: "Invalid character"
            },
            EPhone1: {
                specialcharactor: "Invalid character"
            },
            EPhone2: {
                specialcharactor: "Invalid character"
            },
            EMobile1: {
                specialcharactor: "Invalid character"
            },
            EMobile2: {
                specialcharactor: "Invalid character"
            },
            EGST: {
                specialcharactor: "Invalid character"
            },
            //Policy
            txtpolicyname: {
                required: "Please enter policy name",
                specialcharactor: "Invalid character"
            },
            txtpolicydesc: {
                specialcharactor: "Invalid character"
            },
            // create Rules valiadtion begin here
            txtrulesname: {
                required: "Please enter policy name",
                specialcharactor: "Invalid character"
            },
            txtdescription: {
                specialcharactor: "Invalid character"
            },
            txtcpuusage: {
                required: "Please enter CPU Usage",
                specialcharactor: "Invalid character"
            },
            txtmemoryusage: {
                required: "Please enter memory usage",
                specialcharactor: "Invalid character"
            },
            txtdiskusage: {
                required: "Please enter disk usage",
                specialcharactor: "Invalid character"
            },
            txtcputhresold: {
                required: "Please enter cpu usage",
                specialcharactor: "Invalid character"
            },
            txtmemorythresold: {
                required: "Please enter memory usage",
                specialcharactor: "Invalid character"
            },
            drp_process: {
                required: "Please Select process"
            },
            process_name: { 
                required: "Please enter process name"
            },
            share_delete_days: {
                required: "please select share delete day"
            },
            userrulesdayalert: {
                required: "Please select password changed day"
            },
            drploginuser: {
                required: "Please select user"
            },
            txt_event_id_new: {
                required: "Please enter event id ",
                specialcharactor: "Invalid character"
            },
            txtfwsourceport: {
                digits: "Please enter onlu digits"
            },
            txtfwdestinationip: {
                required: "Please enter destination ip "
            },
            //NAC policy
            nacpolicyname: {
                required: "Please enter NAC policy name",
                specialcharactor: "Invalid character"
            },
            nacip: {
                required: "Please enter Ip address",
                Ipaddress: "Invalid IP Address format"
            },
            nacpolicydesc: {
                specialcharactor: "Invalid character"
            },
            //Report  ==  agent information
            //Report  ==  modem usage
            searchvalue: {
                specialcharactor: "Invalid character"
            },
            serchvalue: {
                specialcharactor: "Special character is not allowed"
            },
            //reports ==  data leakage=> system data model
            IP: {
                Ipaddress: "Invalid IP Address format"
            },
            //reports ==  data leakage=> raw data model
            IP1: {
                Ipaddress: "Invalid IP Address format"
            },
            serachvalue: {
                specialcharactor: "Invalid character"
            },
            //reports ==  data leakage=> reovable data usage model
            IP3: {
                Ipaddress: "Invalid IP Address format"
            },
            searchvalue3: {
                specialcharactor: "Invalid character"
            },
            //reports ==  data leakage=> printer usage model
            IP4: {
                Ipaddress: "Invalid IP Address format"
            },
            searchvalue4: {
                specialcharactor: "Invalid character"
            },
            //reports ==  audit trail
            searchvalue1: {
                specialcharactor: "Invalid character"
            },
            searchvalue2: {
                specialcharactor: "Invalid character"
            },
            ip2: {
                Ipaddress: "Invalid IP Address format"
            },
            //reports  event monitoring
            txtsearcheventid: {
                digits:"Only digits allowed"  
            },
            txtsearch: {
                specialcharactor: "Invalid character"
            },
            //report firewall status report
            ip1: {
                Ipaddress: "Invalid IP Address format"
            },
            //report software information
            txtsearchsoft: {
                specialcharactor: "Invalid character"
            },
            IP2: {
                Ipaddress: "Invalid IP Address format"
            },
            serachvalue2: {
                specialcharactor: "Invalid character"
            },
            serachsoft: {
                specialcharactor: "Invalid character"
            },
            searchsysgenrpt: {
                specialcharactor: "Invalid character"
            },
            txtproductserch: {
                specialcharactor: "Invalid character"
            },
            drpkeyIP: {
                Ipaddress: "Invalid IP Address format"
            },
            drpipcnt: {
                Ipaddress: "Invalid IP Address format"
            },
            txtserchvalue: {
                specialcharactor: "Invalid character"
            },
            sdvalue: {
                specialcharactor: "Invalid character"
            },
            drpkip: {
                Ipaddress: "Invalid IP Address format"
            },
            drpgenerateip: {
                Ipaddress: "Invalid IP Address format"
            },
            txtgeneratesearach: {
                specialcharactor: "Invalid character"
            },
            Svalue: {
                specialcharactor: "Invalid character"
            },
            drpip1: {
                Ipaddress: "Invalid IP Address format"
            },
            Svalue1: {
                specialcharactor: "Invalid character"
            },
            drpip2: {
                Ipaddress: "Invalid IP Address format"
            },
            Svalue2: {
                specialcharactor: "Invalid character"
            },
            portnumber: {
                required: "Please enter port number",
                digits: "Only digits are allowed"
            },
            portname: {
                required: "Please enter port name",
                specialcharactor: "Invalid character"
            },
            //Report Network statistics
            drppip: {
                Ipaddress: "Invalid IP Address format"
            },
            //report enumerate files 
            drpIP: {
                Ipaddress: "Invalid IP Address format"
            },
            Addnewextention: {
                specialcharactor: "Invalid character"
            },
            //report policy apply
            drpip11: {
                Ipaddress: "Invalid IP Address format"
            },
            //setting_organization_structure
            txtlevelname: {
                required: "Please enter level name",
                specialcharactor: "Invalid character"
            },
            etxtleave: {
                required: "Please enter level name",
                specialcharactor: "Invalid character"
            },
            txt_ouname_add_branch_unit: {
                required: "Please enter branch unit",
                specialcharactor: "Invalid character"
            },
            editbranch1: {
                required: "Please enter branch unit",
                specialcharactor: "Invalid character"
            },
            drpUip: {
                Ipaddress: "Invalid IP Address format"
            },
            //setting_user_mgmt
            group_name: {
                required: "Please enter group name",
                specialcharactor: "Invalid character"
            },
            description: {
                specialcharactor: "Invalid character"
            },
            grouptype: {
                required: "Please select group type"
            },
            usertype: {
                required: "Please select user type"
            },
            groupname: {
                required: "Please select group name",
                specialcharactor: "Invalid character"
            },
            emailid: {
                required: "Please enter emailid",
                emailvalid: "Please enter valid email"
            },
            userpassword: {
                required: "Please enter password",
                passwordmatch: "Please enter Password at least one digit/lowercase/uppercase letter and be at least 8 characters long",
                minlength: "Please enter minimum 8 digits",
                maxlength: "Please enter maximum 20 digits"
            },
            contactno: {
                required: "Please enter contact no",
                number: "Please enter digits only",
                minlength: "Please enter 10 digits only",
                maxlength: "Please enter 10 digits only"
            },
            user_nameedit: {
                required: "Please enter user name",
                specialcharactor: "Invalid character"
            },
            usertypeedit: {
                required: "Please select user type"
            },
            groupnameedit: {
                required: "Please select group name"
            },
            emailidedit: {
                required: "Please enter emailid",
                emailvalid: "Please enter valid email"
            },
            userpasswordedit: {
                required: "Please enter password",
                passwordmatch: "Please enter Password at least one digit/lowercase/uppercase letter and be at least 8 characters long",
                minlength: "Please enter minimum 8 digits",
                maxlength: "Please enter maximum 20 digits"
            },
            contactnoedit: {
                required: "Please enter contact no",
                number: "Please enter digits only",
                minlength: "Please enter 10 digits only",
                maxlength: "Please enter 10 digits only"
            },
            securityquestion1: {
                required: "Please select security question"
            },
            securityanswer1: {
                required: "Please enter security answer",
                specialcharactor: "Invalid character"
            },
            oldpassword: {
                required: "Please enter password",
                passwordmatch: "Please enter Password at least one digit/lowercase/uppercase letter and be at least 8 characters long",
                minlength: "Please enter minimum 8 digits",
                maxlength: "Please enter maximum 20 digits"
            },
            newpassword: {
                required: "Please enter password",
                passwordmatch: "Please enter Password at least one digit/lowercase/uppercase letter and be at least 8 characters long",
                minlength: "Please enter minimum 8 digits",
                maxlength: "Please enter maximum 20 digits"
            },
            confirmpassword: {
                required: "Please enter password",
                equalTo: "New Password and Confirm Password do not match"
            },
            tempname: {
                required: "Please enter template name",
                specialcharactor: "Invalid character"
            },
            edittempname: {
                required: "Please enter template name",
                specialcharactor: "Invalid character"
            },
            txtemailto: {
                email: "Please enter valid email"
            },
            txtemailcc: {
                email: "Please enter valid email"
            },
            txteditemailto: {
                email: "Please enter valid email"
            },
            txteditemailcc: {
                email: "Please enter valid email"
            },
            txt_task_name: {
                required: "Please enter task name",
                specialcharactor: "Invalid character"
            },
            txt_task_desc: {
                specialcharactor: "Invalid character"
            },
            txt_activity_file_32: {
                required: "Please enter file 32-bit",
                specialcharactor: "Invalid character"
            },
            txt_activity_destination_path_32: {
                required: "Please enter destination path",
                Path: "Please enter valid path"
            },
            txt_activity_file_64: {
                required: "Please enter file 64-bit",
                specialcharactor: "Invalid character"
            },
            txt_activity_destination_path_64: {
                required: "Please enter destination path",
                Path: "Please enter valid path"
            },
            txt_activity_command_32: {
                required: "Please enter command 32-bit",
                specialcharactor: "Invalid character"
            },
            txt_activity_command_64: {
                required: "Please enter command 64-bit",
                specialcharactor: "Invalid character"
            },
            txt_activity_working_directory: {
                Path: "Please enter valid path"
            },
            txt_source_folder_path: {
                required: "Please enter source folder path",
                Path: "Please enter valid path"
            },
            txt_source_filename: {
                required: "Please enter source file name",
                specialcharactor: "Invalid character"
            },
            txt_server_folder_path: {
                specialcharactor: "Invalid character"
            },
            txt_server_filename: {
                required: "Please enter server file name",
                specialcharactor: "Invalid character"
            },
            txtsearchSW: {
                specialcharactor: "Invalid character"
            },
            drpip15: {
                Ipaddress: "Enter valid IP address"
            },
            drpsname15: {
                specialcharactor: "Invalid character"
            },
            drpdname15: {
                specialcharactor: "Invalid character"
            },
            txtServerIP: {
                required: "Please Enter valid Ip",
                Ipaddress: "Please Enter valid Ip"
            },
            txtFolder: {
                required: "Please Enter valid Path",
                Path: "Please Enter Valid  Path"
            },
            //flag on off
            filenamelist: {
                required: "Please select filename"
            },
            sectionlist: {
                required: "Please select section"
            },
            propertylist: {
                required: "Please select flag name"
            },
            flag_action: {
                required: "Please select action"
            },
            drpou: {
                required: "Please select branch/unit"
            },
            drpou_policy: {
                required: "Please select branch/unit"
            },
            flagvalues: {
                required: "Please enter flag value"
            },
            txtattachment_path: {
                required: "Please enter path",
                specialcharactor: "Invalid character"
            },
            txtvname: {
                specialcharactor: "Invalid character"
            },
            highlight: function (element) {
                jQuery(element).closest('.form-group').removeClass('has-success').addClass('has-error');
                $(document).find('label.error').css('display', 'block');
            }
        },
        errorPlacement: function (error, element) {
            var message = document.getElementById('HardikerrMessage');
            var Newmessage = document.getElementById('errMessage');
            var pos = document.getElementById('All_radio');
            if (element.is(":radio")) {
                error.appendTo(message);
            } else if (element.is(":checkbox")) {
                error.appendTo(Newmessage);
            } else {
                error.insertAfter(element); // This is the default behavior
            }
        },
    });
}
