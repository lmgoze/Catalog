const uri = 'items';
let todos = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function validateFunction() {
  let fname = document.getElementById("add-fname").value;
  let lname = document.getElementById("add-lname").value;
  //submitOK = "true";

  if (fname.length > 20) {
    alert("The First Name may have no more than 20 characters");
    //submitOK = "false";
  } 
  if (lname.length > 20) {
    alert("The Last Name may have no more than 20 characters");
    submitOK = "false";
  } 

  // if (submitOK == "false") {
  //  return false;
  // }

}

function addItem() {
  
  const addFNameTextbox = document.getElementById('add-fname');
  const addLNameTextbox = document.getElementById('add-lname');
  const addEmailTextbox = document.getElementById('add-email');
  const addPhoneTextbox = document.getElementById('add-phone');
  const addAddressTextbox = document.getElementById('add-address');
  const addZipCodeTextbox = document.getElementById('add-zipCode');
  
  validateFunction();

  const item = {
    firstname: addFNameTextbox.value.trim(),
    lastname: addLNameTextbox.value.trim(),
    email: addEmailTextbox.value.trim(),
    phonenumber: addPhoneTextbox.value.trim(),
    address: addAddressTextbox.value.trim(),
    zipcode: addZipCodeTextbox.value.trim(),
  };
  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      addFNameTextbox.value = '';
      addLNameTextbox.value = '';
      addEmailTextbox.value = '';
      addPhoneTextbox.value = '';
      addAddressTextbox.value = '';
      addZipCodeTextbox.value = '';

    })
    .catch(error => console.error('Unable to add item.', error));
};

function deleteItem(event) {
  event.preventDefault();
  const providedId = event.target.dataset.itemId
  fetch(`${uri}/${providedId}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(event) {
  event.preventDefault();
  
  console.dir(event)
  console.dir(event.target)
  console.log(event.target.dataset)
  const providedId = event.target.dataset.itemId

  const item = todos.find(item => item.id === providedId);

  document.getElementById('edit-fname').value = item.firstName;
  document.getElementById('edit-lname').value = item.lastName;
  document.getElementById('edit-email').value = item.email;
  document.getElementById('edit-phone').value = item.phoneNumber;
  document.getElementById('edit-address').value = item.address;
  document.getElementById('edit-zipCode').value = item.zipCode;
  document.getElementById('edit-id').value = item.id;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    id: parseInt(itemId, 10),
    firstname: document.getElementById('edit-fname').value.trim(),
    lastname: document.getElementById('edit-lname').value.trim(),
    email: document.getElementById('edit-email').value.trim(),
    phonenumber: document.getElementById('edit-phone').value.trim(),
    address: document.getElementById('edit-address').value.trim(),
    zipcode: document.getElementById('edit-zipCode').value.trim()
  };
  fetch(`${uri}/${itemId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to update item.', error));
  closeInput();
  return false;
}


function closeInput() {
   document.getElementById('editForm').style.display = 'none';
   document.alert("Close Input");
}

function _displayCount(itemCount) {
  //if statement (ternary statement) condition ? if : else
  const name = (itemCount === 1) ? 'schedule' : 'schedules';
  // What the ternary statement is doing:
  // let name;
  // if(itemCount === 1){
  //   name = 'catalog'
  // }else{
  //   name = 'catalogs'
  // }

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('todos');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {

    let isCompleteCheckbox = document.createElement('input');
    isCompleteCheckbox.type = 'checkbox';
    isCompleteCheckbox.disabled = true;
    isCompleteCheckbox.checked = item.isComplete;

    // console.dir(isCompleteCheckbox)
    // console.log(isCompleteCheckbox)

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.dataset.itemId = item.id;
    editButton.addEventListener('click', displayEditForm)


    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.dataset.itemId = item.id;
    deleteButton.addEventListener('click', deleteItem)


    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    let fnameNode = document.createTextNode(item.firstName);
    td1.appendChild(fnameNode);

    let td2 = tr.insertCell(1);
    let lnameNode = document.createTextNode(item.lastName);
    td2.appendChild(lnameNode);

    let td3 = tr.insertCell(2);
    let emailNode = document.createTextNode(item.email);
    td3.appendChild(emailNode);

    let td4 = tr.insertCell(3);
    let phoneNumberNode = document.createTextNode(item.phoneNumber);
    td4.appendChild(phoneNumberNode);
    
    let td5 = tr.insertCell(4);
    let addressNode = document.createTextNode(item.address);
    td5.appendChild(addressNode);

    let td6 = tr.insertCell(5);
    let zipCodeNode = document.createTextNode(item.zipCode);
    td6.appendChild(zipCodeNode);

    let td7 = tr.insertCell(6);
    td7.appendChild(editButton);

    let td8 = tr.insertCell(7);
    td8.appendChild(deleteButton);
  });

  todos = data;
  
}