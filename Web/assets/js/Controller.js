app.controller('CRUDController', ['$scope', 'CRUDService', function ($scope, CRUDService) {

    $scope.EmpModel = {
        Id: 0,
        Salary: 0,
        Name: '',
    };
    loadRecords();


    $scope.canAdd = true;

    //Function to load all Employee records
    function loadRecords() {
        var promiseGet = CRUDService.getEmployees(); //The MEthod Call from service

        promiseGet.then(function (response) {
            console.log(response.data);
            $scope.EmpList = angular.copy(response.data)
        },

            function (err) {
                console.log("Err" + err);
            });
    }

    $scope.AddData = function () {
        var _emp = {
            Id: $scope.EmpList.length + 1,
            Name: $scope.EmpModel.Name,
            Salary: $scope.EmpModel.Salary
        };
        var promisePost = CRUDService.postEmployee(_emp);
        promisePost.then(function (data) {
            loadRecords();
        }, function (err) {
            console.log("Err" + err);
        });
        ClearModel();
    }

    $scope.DeleteData = function (emp) {
        var promiseDelete = CRUDService.deleteEmployee(emp.Id);
        promiseDelete.then(function (data) {
            loadRecords();
            ClearModel();
        }, function (err) {
            console.log("Err" + err);
        });


    }

    $scope.BindSelectedData = function (emp) {
        $scope.EmpModel.Id = emp.Id;
        $scope.EmpModel.Name = emp.Name;
        $scope.EmpModel.Salary = emp.Salary;
        $scope.canAdd = false;

    }

    $scope.UpdateData = function () {
        var _emp = {
            Id: $scope.EmpModel.Id,
            Name: $scope.EmpModel.Name,
            Salary: $scope.EmpModel.Salary
        };
        var promisePost = CRUDService.updateEmployee(_emp);
        promisePost.then(function (data) {
            loadRecords();
        }, function (err) {
            console.log("Err" + err);
        });
        ClearModel();

    }

    $scope.RefreshData = function () {
        ClearModel();
    }

    function ClearModel() {
        $scope.EmpModel.Id = 0;
        $scope.EmpModel.Name = '';
        $scope.EmpModel.Salary = 0;
        $scope.canAdd = true;
    }
}]);