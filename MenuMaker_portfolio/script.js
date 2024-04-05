
  
import { initializeApp } from "https://www.gstatic.com/firebasejs/10.10.0/firebase-app.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/10.10.0/firebase-analytics.js";
import { getFirestore } from "https://www.gstatic.com/firebasejs/10.10.0/firebase-firestore.js";
import { getDatabase, ref, set } from "https://www.gstatic.com/firebasejs/10.10.0/firebase-database.js";

  const firebaseConfig = {
    apiKey: "AIzaSyCjIhHUZXX4AaFGfy2SZ9c5gXfUNAJ7KKI",
    authDomain: "menu-maker-5d34a.firebaseapp.com",
    projectId: "menu-maker-5d34a",
    storageBucket: "menu-maker-5d34a.appspot.com",
    messagingSenderId: "48004184431",
    appId: "1:48004184431:web:74b9015ca508a340d9509a",
    measurementId: "G-GFZ3ZNFPB3"
  };

  
// Initialize Firebase
const app = initializeApp(firebaseConfig);

// Listen for submit event
document.getElementById('contact-form').addEventListener('submit', formSubmit);

// Submit form
function formSubmit(e) {
  e.preventDefault();
  
  // Get Values from the DOM
  const name = document.getElementById('name').value;
  const email = document.getElementById('email').value;
  const subject = document.getElementById('subject').value;
  const message = document.getElementById('message').value;

  // Send Message to Firebase
  sendMessage(name, email, subject, message);
}

// Send Message to Firebase
function sendMessage(name, email, subject, message) {
  const database = getDatabase();

  set(ref(database, 'contacts/' + Math.floor(Math.random() * 10000000)), {
    name: name,
    email: email,
    subject: subject,
    message: message
  })
  .then(() => {
    // Show Alert Message
    alert('Message sent successfully!');
    // Reset form
    document.getElementById('contact-form').reset();
  })
  .catch((error) => {
    console.error('Error sending message: ', error);
    alert('An error occurred while sending the message. Please try again later.');
  });
}