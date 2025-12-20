function prepareAdd() {
    document.getElementById('vehicleModalLabel').innerText = 'Dodaj pojazd';
    document.getElementById('vehicleForm').action = '?handler=Add';
    
    document.getElementById('NewVehicle_Id').value = '00000000-0000-0000-0000-000000000000';
    document.getElementById('NewVehicle_RegistrationNumber').value = '';
    document.getElementById('NewVehicle_Brand').value = '';
    document.getElementById('NewVehicle_Model').value = '';
    document.getElementById('NewVehicle_YearOfProduction').value = '';
    document.getElementById('NewVehicle_InspectionDate').value = new Date().toISOString().split('T')[0];
    document.getElementById('NewVehicle_Owner').value = '';
}

function prepareEdit(id, reg, brand, model, year, date, owner) {
    document.getElementById('vehicleModalLabel').innerText = 'Edytuj pojazd';
    document.getElementById('vehicleForm').action = '?handler=Edit';
    
    document.getElementById('NewVehicle_Id').value = id;
    document.getElementById('NewVehicle_RegistrationNumber').value = reg;
    document.getElementById('NewVehicle_Brand').value = brand;
    document.getElementById('NewVehicle_Model').value = model;
    document.getElementById('NewVehicle_YearOfProduction').value = year;
    document.getElementById('NewVehicle_InspectionDate').value = date;
    document.getElementById('NewVehicle_Owner').value = owner;
}

function prepareDelete(id) {
    document.getElementById('deleteId').value = id;
}
