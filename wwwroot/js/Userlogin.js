const uri = '/MyTask';
const userUri = '/User/UserLogin';
const taskUri = '/MyTask/Task';
function send() {
    const NameTextbox = document.getElementById('name');
    const MailTextbox = document.getElementById('mail');
    const PasswordTextbox = document.getElementById('password');
    const isAdmin = document.getElementById('isAdmin');
    
    const user = {
        userName: "string",
       mail:MailTextbox.value,
       password:  PasswordTextbox.value,
       isAdmin: isAdmin.value,
        
    };

    fetch(userUri, {
        method: 'POST',
        headers: {
            'Accept':'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(response => response.text())
        .then((text) => {
            
            MailTextbox.value = '';
            PasswordTextbox.value = '';
            token = text;
            text = text.replace(/"/g, '')
            sessionStorage.setItem('token', text)
            alert(text+user);
            if(isAdmin.value=='true') {
                location.href = "admin.html"
            }
            else{
            location.href = "user.html"}
        })
        .catch(error => console.error('Unable to add item.', error));
}
