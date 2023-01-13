var table;

$(document).ready(function () {
  table=  $('#myTable').DataTable({
        "ajax":{
          "url":"/Admin/Product/AllProduct"
            },           
            "columns": [
                {"data": 'name' },
                {"data": 'description' },
                {"data": 'price' }              
            ]     
        });
});