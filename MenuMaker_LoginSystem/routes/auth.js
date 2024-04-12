const express = require('express');
const bcrypt = require('bcrypt');
const router = express.Router();
const User = require('../models/user');

router.get('/login', (req, res) => {
  res.render('login');
});

router.post('/login', async (req, res) => {
    const { username, password } = req.body;
    try {
      const user = await User.findOne({ username });
      if (user && await bcrypt.compare(password, user.password)) {
        req.session.userId = user._id;
        res.redirect('/dashboard');
      } else {
        res.render('login', { error: 'Invalid username or password.' }); // Pass error message to the login view
      }
    } catch (error) {
      console.error(error);
      res.redirect('/login');
    }
  });

router.get('/register', (req, res) => {
  res.render('register');
});

router.post('/register', async (req, res) => {
    const { username, password, name, email, phone } = req.body;
    try {
      const hashedPassword = await bcrypt.hash(password, 10);
      const newUser = new User({ username, password: hashedPassword, name, email, phone });
      await newUser.save();
      res.redirect('/login');
    } catch (error) {
      console.error(error);
      res.redirect('/register');
    }
  });

router.get('/logout', (req, res) => {
  req.session.destroy(err => {
    if (err) {
      console.error(err);
    }
    res.redirect('/login');
  });
});

module.exports = router;
