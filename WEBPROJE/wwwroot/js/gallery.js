function createImage(src) {

    const image = document.createElement('img');
    image.src = src;
    return image;
}

var currentid = '';

function likeClick() {

    console.log(currentid);
}
function ClickedPhoto() {
    console.log("aaa");
}
function onThumbnailClick(event) {

    console.log(event);
    const image = createImage(event.srcElement.currentSrc);
    modalView.innerHTML = ' ';
    modalView.appendChild(image);
    modalView.classList.remove('hidden');

    const sidemenu = document.getElementById('sidemenu');
    sidemenu.classList.remove('hidden');
}

function onModalClick() {

    const sidemenu = document.getElementById('sidemenu');
    sidemenu.classList.add('hidden');
    console.log(sidemenu);
    hideModal();

}
window.addEventListener('scroll', function (e) {
    container = document.getElementById('modal-view');
    container.classList.toggle('scrolling', window.scrollY > 0); // could be a larger threshold
    container.style.top += 1;
});


function hideModal() {
    modalView.classList.add('hidden');

    modalView.innerHTML = '';
    document.removeEventListener('keydown', nextPhoto);
}

let currentIndex = null;
const albumView = document.querySelector('#album-view');




const modalView = document.querySelector('#modal-view');
modalView.addEventListener('click', onModalClick);

console.log("aaa");

