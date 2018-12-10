app.service('CRUDService', function ($http) {

    this.apiBaseUrl = "http://localhost:5000/api/V1/employee"

    //Get All Employees
    this.getEmployees = function () {
        return $http.get(this.apiBaseUrl);
    }

    //Add Employee
    this.postEmployee = function (Employee) {
        var request = $http({
            method: "post",
            url: this.apiBaseUrl,
            data: Employee
        });
        return request;
    }

    //Update Employee
    this.updateEmployee = function (Employee) {
        var request = $http({
            method: "put",
            url: this.apiBaseUrl + "/" + Employee.Id,
            data: Employee
        });
        return request;
    }

    //Get Employee
    this.getEmployeebyId = function (EmpNo) {
        return $http.get(this.apiBaseUrl + "/" + EmpNo);
    }

    //Delete Employee
    this.deleteEmployee = function (EmpNo) {
        var request = $http({
            method: "delete",
            url: this.apiBaseUrl + "/" + EmpNo
        });
        return request;
    }


});