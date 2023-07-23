const uri = '/MyTask';
const userUri = '/Admin/UserLogin';
const taskUri = '/MyTask/Task';
function send() {
    const NameTextbox = document.getElementById('name');
    const PasswordTextbox = document.getElementById('password');

    const user = {
        Name: NameTextbox.value,
        Password: PasswordTextbox.value,
        id:0
        
    };

    fetch('/UserLogin', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(response => response.json())
        .then(() => {
            NameTextbox.value = '';
            PasswordTextbox.value = '';
            token = text;
            text = text.replace(/"/g, '')
            sessionStorage.setItem('token', text)
            alert(text);
            location.href = "user.html"
        })
        .catch(error => console.error('Unable to add item.', error));
}
