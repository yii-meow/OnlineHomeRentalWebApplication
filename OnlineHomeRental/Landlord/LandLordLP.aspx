﻿<!doctype html>
<html lang="en">
<head>
    <title>OnlineHomeRental</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="stylesheet" href="https://unicons.iconscout.com/release/v2.1.9/css/unicons.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="/CSS/LandLordLP.css" />
</head>
<body>
    <div class="section">
        <div class="container">
            <div class="row full-height justify-content-center">
                <div class="col-12 text-center align-self-center py-5">
                    <div class="section pb-5 pt-5 pt-sm-2 text-center">
                        <h3 class="mb-0 pb-2">Welcome, LandLord!</h3>
                        <h6 class="mb-0 pb-3"><span>Log In </span><span>Sign Up</span></h6>
                        <input class="checkbox" type="checkbox" id="reg-log" name="reg-log" />
                        <label for="reg-log"></label>
                        <div class="card-3d-wrap mx-auto">
                            <div class="card-3d-wrapper">
                                <div class="card-front">
                                    <div class="center-wrap">
                                        <div class="dropdown">
                                            <!-- The button with the icon -->
                                            <button class="dropdown-button" onclick="toggleDropdown()">
                                                <i class="input-icon uil uil-user icon"></i>
                                                <!-- The dropdown content -->
                                                <div class="dropdown-content" id="myDropdown" style="display: none; left: -150px; top: -35px;">
                                                    <!-- Dropdown items -->
                                                    <a href="/Landlord/LandLordLP.aspx" class="dropdown-item">LandLord</a>
                                                    <a href="/Tenant/TenantLP.aspx" class="dropdown-item">Tenant</a>
                                                </div>
                                            </button>
                                        </div>
                                        <div class="section text-center">
                                            <h4 class="mb-4 pb-3">Log In</h4>
                                            <div class="form-group">
                                                <input type="email" class="form-style" placeholder="Email">
                                                <i class="input-icon uil uil-at"></i>
                                            </div>
                                            <div class="form-group mt-2">
                                                <input type="password" class="form-style" placeholder="Password">
                                                <i class="input-icon uil uil-lock-alt"></i>
                                            </div>
                                            <a href="Homepage.aspx" class="btn mt-4">Login</a>
                                            <p class="mb-0 mt-4 text-center"><a href="" class="link">Forgot your password?</a></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-back">
                                    <div class="center-wrap">
                                        <div class="section text-center">

                                            <h4 class="mb-3 pb-3" style="margin-top: 50px">Sign Up</h4>
                                            <div class="form-group">
                                                <input type="text" class="form-style" placeholder="Full Name">
                                                <i class="input-icon uil uil-user"></i>
                                            </div>
                                            <div class="form-group mt-2">
                                                <input type="text" class="form-style" placeholder="Gender">
                                                <i class="input-icon uil uil-mars"></i>
                                            </div>
                                            <div class="form-group mt-2">
                                                <input type="tel" class="form-style" placeholder="Phone Number">
                                                <i class="input-icon uil uil-phone"></i>
                                            </div>
                                            <div class="form-group mt-2">
                                                <input type="email" class="form-style" placeholder="Email">
                                                <i class="input-icon uil uil-at"></i>
                                            </div>
                                            <div class="form-group mt-2">
                                                <input type="password" class="form-style" placeholder="Password">
                                                <i class="input-icon uil uil-lock-alt"></i>
                                            </div>
                                            <a href="" class="btn mt-4">Register</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        // JavaScript to toggle the dropdown
        function toggleDropdown() {
            var dropdown = document.getElementById("myDropdown");
            dropdown.style.display = (dropdown.style.display === "block") ? "none" : "block";
        }

        // Close the dropdown if the user clicks outside of it
        window.onclick = function (event) {
            if (!event.target.matches('.icon') && !event.target.matches('.dropdown-button')) {
                var dropdowns = document.getElementsByClassName("dropdown-content");
                for (var i = 0; i < dropdowns.length; i++) {
                    var openDropdown = dropdowns[i];
                    if (openDropdown.style.display === "block") {
                        openDropdown.style.display = "none";
                    }
                }
            }
        }
    </script>

</body>
</html>
