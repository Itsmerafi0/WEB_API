
let query1 = document.querySelector(".main:first-child");
let query2 = document.querySelector("ul li:nth-child(1)");
let query3 = document.querySelector("ul li:nth-child(2)");


function baris() {
    query1.innerHTML = "Power Ranger";
    query1.style.backgroundColor = "green";
    query1.style.color = "white";
    setTimeout(resetStyles, 6000);
}

function baris1() {
    alert("Baris 1 Berubah")
    query2.innerHTML = "Ultrament";
    query2.style.backgroundColor = "blue";
    query2.style.color = "white"
    setTimeout(resetStyles, 6000);
}

function baris2() {
    alert("Baris 2 Berubah")
    query3.innerHTML = "Kamen Rider";
    query3.style.backgroundColor = "black";
    query3.style.color = "white"
    setTimeout(resetStyles, 6000);
}

query1.addEventListener('mouseover', () => {
    query1.innerHTML = "Data Berubah";
});

query1.addEventListener('mouseout', () => {
    query1.innerHTML = "Main Content"
});

query1.add
let originalBackground = {
    main: "#B8860B",
    sidebar1: "#006400",
    sidebar2: "#BA55D3"
};

const resetStyles = () => {
    query1.innerHTML = "Main Content";
    query1.style.backgroundColor = originalBackground.main;
    query1.style.color = "";

    query2.innerHTML = "SideBar1";
    query2.style.backgroundColor = originalBackground.sidebar1;
    query2.style.color = "";

    query3.innerHTML = "SideBar2";
    query3.style.backgroundColor = originalBackground.sidebar2;
    query3.style.color = "";

}
