var token=sessionStorage.getItem('token');
alert(token);
let users = [];
const uriUser = '/User';
function _displayUser(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    // _displayCount(data.length);

    const button = document.createElement('button');
console.log(data);
    data.forEach(item => {
        

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick',"deleteItem('" + item.mail + "')");

        let tr = tBody.insertRow();
        let textNode = document.createTextNode(item.userName);
        let td1 = tr.insertCell(0);
        td1.appendChild(textNode);
        let textNode2 = document.createTextNode(item.password);
        let td2 = tr.insertCell(1);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);

       
    });

    users = data;
}
function addUsers() {
    fetch(uriUser,{method:'GET', headers:{'Authorization': `Bearer ${token}`}})
        .then(response =>
            response.json())
        .then(data => _displayUser(data))
        .catch(error => console.error('Unable to get items.', error));
}
function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addPassword=document.getElementById('add-password');
   

    const item = {
        
            userName: addNameTextbox.value.trim(),
            mail:  addNameTextbox.value.trim(),
            password: addPassword.value.trim(),
            isAdmin: "false"
          
    };

    fetch(uriUser+'/GenerateUser', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(data => addUsers())
        .catch(error => console.error('Unable to add item.', error));
      
}
function deleteItem(id) {
    console.log(id);
    fetch(`${uriUser}/${id}`, {
            method: 'DELETE',
            'Accept': 'application/json',
            'Content-Type': 'application/json',
             headers:{'Authorization': `Bearer ${token}`}
        })
        .then(() => addUsers())
        .catch(error => console.error('Unable to delete item.', error));
}
