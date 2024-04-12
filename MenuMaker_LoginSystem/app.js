const express = require('express');
const mongoose = require('mongoose');
const path = require('path');
const session = require('express-session');
const indexRoutes = require('./routes/index');
const authRoutes = require('./routes/auth');
const dashboardRouter = require('./routes/dashboard');

const app = express();

mongoose.connect('mongodb://localhost:27017/node-login-system', { useNewUrlParser: true, useUnifiedTopology: true })
  .then(() => {
    console.log('Connected to MongoDB');
  })
  .catch(err => {
    console.error('Error connecting to MongoDB', err);
  });


app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');
app.use(express.urlencoded({ extended: true }));
app.use(session({
  secret: 'secret',
  resave: false,
  saveUninitialized: true
}));

app.use('/', indexRoutes);
app.use('/', authRoutes);
app.use('/', dashboardRouter);



app.listen(3000, () => {
  console.log('Server started on port 3000');
});
