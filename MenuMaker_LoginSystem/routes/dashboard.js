// routes/dashboard.js

const express = require('express');
const router = express.Router();
const User = require('../models/user'); // Assuming User model exists

// Dashboard route
router.get('/dashboard', async (req, res) => {
  try {
    // Check if user is logged in
    if (req.session.userId) {
      // Retrieve user information from database
      const user = await User.findById(req.session.userId); // Assuming you have a findById method in your User model

      if (!user) {
        throw new Error('User not found');
      }

      // Render dashboard view with user information
      res.render('dashboard', { username: user.username, user: user });
    } else {
      res.redirect('/login'); // Redirect to login if not logged in
    }
  } catch (error) {
    // Handle error
    console.error('Error fetching user:', error);
    res.status(500).send('Error fetching user information');
  }
});

module.exports = router;
