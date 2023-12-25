<%@ Page Language="C#" MasterPageFile="~/Tenant/TenantMenu.Master" AutoEventWireup="true" CodeBehind="~/Tenant/Homepage.aspx.cs" Inherits="OnlineHomeRental.Tenant.Homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Header Start -->
    <div class="container-fluid header bg-white p-0 ">
        <div class="row g-0 align-items-center flex-column-reverse flex-md-row ">
            <div class="col-md-6 p-5 mt-lg-5 ">
                <h1 class="display-5 animated fadeIn mb-4 ">Welcome to <span class="text-primary">EZRent</span></h1>
                <p class="animated fadeIn mb-4 pb-2 justify-text line-spacing">
                    Find your perfect home away from home! Our platform is exclusively designed for TARUMT students to discover comfortable and convenient rental spaces. Whether you're seeking a peaceful retreat or a vibrant community, explore a range
                        of accommodations tailored to elevate your college experience. Begin your search today and secure your ideal student living space hassle-free!
                </p>
                <a href="#propertylisting-homepage" class="btn btn-primary py-3 px-4 me-3 animated fadeIn">View Properties Now</a>
            </div>
            <div class="col-md-6 animated fadeIn ">
                <div class="owl-carousel header-carousel ">
                    <div class="owl-carousel-item ">
                        <img class="img-fluid " src="img/carousel-1.jpg " alt="" />
                    </div>
                    <div class="owl-carousel-item ">
                        <img class="img-fluid " src="img/carousel-2.jpg " alt="" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Header End -->


    <!-- Search Start -->
    <div class="container-fluid bg-primary mb-5 wow fadeIn " data-wow-delay="0.1s " style="padding: 35px;">
        <div class="container ">
            <div class="row g-2 ">
                <div class="col-md-10 ">
                    <div class="row g-2 ">
                        <div class="col-md-3">
                            <input type="text" class="form-control border-0 py-3 " placeholder="Enter Your Min Budget" />
                        </div>
                        <div class="col-md-3">
                            <input type="text" class="form-control border-0 py-3 " placeholder="Enter Your Max Budget" />
                        </div>
                        <div class="col-md-3">
                            <select class="form-select border-0 py-3 ">
                                <option selected="">Property Type</option>
                                <option value="1">Residential</option>
                                <option value="2">Flat</option>
                                <option value="3">Apartment</option>
                                <option value="4">Condominium</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <select class="form-select border-0 py-3 ">
                                <option selected="">Location</option>
                                <option value="1">Setapak</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="col-md-2 ">
                    <button class="btn btn-dark border-0 w-100 py-3 ">Search</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Search End -->


    <!-- Category Start -->
    <div class="container-xxl py-5 ">
        <div class="container ">
            <div class="text-center mx-auto mb-5 wow fadeInUp " data-wow-delay="0.1s " style="max-width: 600px;">
                <h1 class="mb-3 ">Property Types</h1>
                <p>Choose from 8 unique property types for your perfect living space.</p>
            </div>
            <div class="row g-4 ">
                <div class="col-lg-3 col-sm-6 wow fadeInUp " data-wow-delay="0.1s ">
                    <a class="cat-item d-block bg-light text-center rounded p-3 " href="#">
                        <div class="rounded p-4 ">
                            <div class="icon mb-3 ">
                                <img class="img-fluid " src="img/icon-luxury.png" alt="Icon" />
                            </div>
                            <h6>Residential</h6>
                            <span>999 Properties</span>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-sm-6 wow fadeInUp " data-wow-delay="0.3s ">
                    <a class="cat-item d-block bg-light text-center rounded p-3 " href="#">
                        <div class="rounded p-4 ">
                            <div class="icon mb-3 ">
                                <img class="img-fluid " src="img/icon-house.png" alt="Icon" />
                            </div>
                            <h6>Flat</h6>
                            <span>999 Properties</span>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-sm-6 wow fadeInUp " data-wow-delay="0.5s ">
                    <a class="cat-item d-block bg-light text-center rounded p-3 " href="#">
                        <div class="rounded p-4 ">
                            <div class="icon mb-3 ">
                                <img class="img-fluid " src="img/icon-apartment.png" alt="Icon" />
                            </div>
                            <h6>Apartment</h6>
                            <span>999 Properties</span>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-sm-6 wow fadeInUp " data-wow-delay="0.7s ">
                    <a class="cat-item d-block bg-light text-center rounded p-3 " href="#">
                        <div class="rounded p-4 ">
                            <div class="icon mb-3 ">
                                <img class="img-fluid " src="img/icon-condominium.png" alt="Icon" />
                            </div>
                            <h6>Condominium</h6>
                            <span>999 Properties</span>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>
    <!-- Category End -->


    <!-- About Start -->
    <div class="container-xxl py-5 ">
        <div class="container ">
            <div class="row g-5 align-items-center ">
                <div class="col-lg-6 wow fadeIn " data-wow-delay="0.1s ">
                    <div class="about-img position-relative overflow-hidden p-5 pe-0 ">
                        <img class="img-fluid w-100 " src="Data/Property.jpg" />
                    </div>
                </div>
                <div class="col-lg-6 wow fadeIn " data-wow-delay="0.5s ">
                    <h1 class="mb-4 justify-text">Your Premier Destination for Ideal Properties</h1>
                    <p class="mb-4 justify-text line-spacing">Welcome to our platform, the ultimate hub for discovering your dream property. We offer a seamless experience tailored to your needs, connecting you with a diverse range of exceptional listings. Find your perfect home hassle-free!</p>
                    <p><i class="fa fa-check text-primary me-3 "></i>Customized Search</p>
                    <p><i class="fa fa-check text-primary me-3 "></i>Verified Listings</p>
                    <p><i class="fa fa-check text-primary me-3 "></i>Expert Assistance</p>
                    <a class="btn btn-primary py-3 px-5 mt-3 " href="#">Read More</a>
                </div>
            </div>
        </div>
    </div>
    <!-- About End -->


    <!-- Property List Start -->
    <div class="container-xxl py-5" id="propertylisting-homepage">
        <div class="container ">
            <div class="row g-0 gx-5 align-items-end ">
                <div class="col-lg-6 ">
                    <div class="text-start mx-auto mb-5 wow slideInLeft " data-wow-delay="0.1s ">
                        <h1 class="mb-3 ">Property Listing</h1>
                        <p class="justify-text line-spacing">Browse through detailed property listings offering a variety of living spaces. Find your perfect home hassle-free with detailed information and images.</p>
                    </div>
                </div>
            </div>
            <div class="tab-content ">
                <div id="tab-1 " class="tab-pane fade show p-0 active ">
                    <div class="row g-4 ">
                        <div class="col-lg-4 col-md-6 wow fadeInUp " data-wow-delay="0.1s ">
                            <div class="property-item rounded overflow-hidden ">
                                <div class="position-relative overflow-hidden ">
                                    <a href="#">
                                        <img class="img-fluid" src="Data/Property.jpg" alt="" /></a>
                                    <div class="bg-white rounded-top text-primary position-absolute start-0 bottom-0 mx-4 pt-1 px-3 ">Property Type</div>
                                </div>
                                <div class="p-4 pb-0">
                                    <h5 class="text-primary mb-3">RM 999.99</h5>
                                    <a class="d-block h5 mb-2 " href="#">Property Name</a>
                                    <p><i class="fa fa-map-marker-alt text-primary me-2 "></i>Property Location</p>
                                </div>
                                <div class="d-flex border-top ">
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-ruler-combined text-primary me-2 "></i>999 Sqft</small>
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-bed text-primary me-2 "></i>9 Bed</small>
                                    <small class="flex-fill text-center py-2 "><i class="fa fa-bath text-primary me-2 "></i>9 Bath</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 wow fadeInUp " data-wow-delay="0.3s ">
                            <div class="property-item rounded overflow-hidden ">
                                <div class="position-relative overflow-hidden ">
                                    <a href="#">
                                        <img class="img-fluid " src="Data/Property.jpg" alt=" " /></a>
                                    <div class="bg-white rounded-top text-primary position-absolute start-0 bottom-0 mx-4 pt-1 px-3 ">Property Type</div>
                                </div>
                                <div class="p-4 pb-0 ">
                                    <h5 class="text-primary mb-3 ">RM 999.99</h5>
                                    <a class="d-block h5 mb-2 " href="#">Property Name</a>
                                    <p><i class="fa fa-map-marker-alt text-primary me-2 "></i>Property Location</p>
                                </div>
                                <div class="d-flex border-top ">
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-ruler-combined text-primary me-2 "></i>999 Sqft</small>
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-bed text-primary me-2 "></i>9 Bed</small>
                                    <small class="flex-fill text-center py-2 "><i class="fa fa-bath text-primary me-2 "></i>9 Bath</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 wow fadeInUp " data-wow-delay="0.5s ">
                            <div class="property-item rounded overflow-hidden ">
                                <div class="position-relative overflow-hidden ">
                                    <a href="#">
                                        <img class="img-fluid " src="Data/Property.jpg" alt=" " /></a>
                                    <div class="bg-white rounded-top text-primary position-absolute start-0 bottom-0 mx-4 pt-1 px-3 ">Property Type</div>
                                </div>
                                <div class="p-4 pb-0 ">
                                    <h5 class="text-primary mb-3 ">RM 999.99</h5>
                                    <a class="d-block h5 mb-2 " href="#">Property Name</a>
                                    <p><i class="fa fa-map-marker-alt text-primary me-2 "></i>Property Location</p>
                                </div>
                                <div class="d-flex border-top ">
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-ruler-combined text-primary me-2 "></i>999 Sqft</small>
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-bed text-primary me-2 "></i>9 Bed</small>
                                    <small class="flex-fill text-center py-2 "><i class="fa fa-bath text-primary me-2 "></i>9 Bath</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 wow fadeInUp " data-wow-delay="0.1s ">
                            <div class="property-item rounded overflow-hidden ">
                                <div class="position-relative overflow-hidden ">
                                    <a href="#">
                                        <img class="img-fluid " src="Data/Property.jpg" alt="" /></a>
                                    <div class="bg-white rounded-top text-primary position-absolute start-0 bottom-0 mx-4 pt-1 px-3 ">Property Type</div>
                                </div>
                                <div class="p-4 pb-0 ">
                                    <h5 class="text-primary mb-3 ">RM 999.99</h5>
                                    <a class="d-block h5 mb-2 " href="#">Property Name</a>
                                    <p><i class="fa fa-map-marker-alt text-primary me-2 "></i>Property Location</p>
                                </div>
                                <div class="d-flex border-top ">
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-ruler-combined text-primary me-2 "></i>999 Sqft</small>
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-bed text-primary me-2 "></i>9 Bed</small>
                                    <small class="flex-fill text-center py-2 "><i class="fa fa-bath text-primary me-2 "></i>9 Bath</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 wow fadeInUp " data-wow-delay="0.3s ">
                            <div class="property-item rounded overflow-hidden ">
                                <div class="position-relative overflow-hidden ">
                                    <a href="#">
                                        <img class="img-fluid " src="Data/Property.jpg" alt="" /></a>
                                    <div class="bg-white rounded-top text-primary position-absolute start-0 bottom-0 mx-4 pt-1 px-3 ">Property Type</div>
                                </div>
                                <div class="p-4 pb-0 ">
                                    <h5 class="text-primary mb-3 ">RM 999.99</h5>
                                    <a class="d-block h5 mb-2 " href="#">Property Name</a>
                                    <p><i class="fa fa-map-marker-alt text-primary me-2 "></i>Property Location</p>
                                </div>
                                <div class="d-flex border-top ">
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-ruler-combined text-primary me-2 "></i>999 Sqft</small>
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-bed text-primary me-2 "></i>9 Bed</small>
                                    <small class="flex-fill text-center py-2 "><i class="fa fa-bath text-primary me-2 "></i>9 Bath</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 wow fadeInUp " data-wow-delay="0.5s ">
                            <div class="property-item rounded overflow-hidden ">
                                <div class="position-relative overflow-hidden ">
                                    <a href="#">
                                        <img class="img-fluid " src="Data/Property.jpg" alt="" /></a>
                                    <div class="bg-white rounded-top text-primary position-absolute start-0 bottom-0 mx-4 pt-1 px-3 ">Property Type</div>
                                </div>
                                <div class="p-4 pb-0 ">
                                    <h5 class="text-primary mb-3 ">RM 999.99</h5>
                                    <a class="d-block h5 mb-2 " href="#">Property Name</a>
                                    <p><i class="fa fa-map-marker-alt text-primary me-2 "></i>Property Location</p>
                                </div>
                                <div class="d-flex border-top ">
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-ruler-combined text-primary me-2 "></i>999 Sqft</small>
                                    <small class="flex-fill text-center border-end py-2 "><i class="fa fa-bed text-primary me-2 "></i>9 Bed</small>
                                    <small class="flex-fill text-center py-2 "><i class="fa fa-bath text-primary me-2 "></i>9 Bath</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 text-center wow fadeInUp " data-wow-delay="0.1s ">
                            <a class="btn btn-primary py-3 px-5 " href="ViewProperties.aspx">View More Properties</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Property List End -->


    <!-- Testimonial Start -->
    <div class="container-xxl py-5 ">
        <div class="container ">
            <div class="text-center mx-auto mb-5 wow fadeInUp " data-wow-delay="0.1s " style="max-width: 600px;">
                <h1 class="mb-3 ">Client Testimonials</h1>
                <p class="line-spacing">Discover what our clients have to say! Explore genuine feedback and testimonials from our valued customers. Their experiences speak volumes about our commitment to exceptional service and satisfaction.</p>
            </div>
            <div class="owl-carousel testimonial-carousel wow fadeInUp " data-wow-delay="0.1s ">
                <div class="testimonial-item bg-light rounded p-3 ">
                    <div class="bg-white border rounded p-4 ">
                        <p>Client's Comment</p>
                        <div class="d-flex align-items-center ">
                            <img class="img-fluid flex-shrink-0 rounded " src="Data/pp.jpg" style="width: 45px; height: 45px;" />
                            <div class="ps-3 ">
                                <h6 class="fw-bold mb-1 ">Client's Name</h6>
                                <small>Client's Gender</small>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="testimonial-item bg-light rounded p-3 ">
                    <div class="bg-white border rounded p-4 ">
                        <p>Client's Comment</p>
                        <div class="d-flex align-items-center ">
                            <img class="img-fluid flex-shrink-0 rounded " src="Data/pp.jpg" style="width: 45px; height: 45px;" />
                            <div class="ps-3 ">
                                <h6 class="fw-bold mb-1">Client's Name</h6>
                                <small>Client's Gender</small>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="testimonial-item bg-light rounded p-3 ">
                    <div class="bg-white border rounded p-4 ">
                        <p>Client's Comment</p>
                        <div class="d-flex align-items-center ">
                            <img class="img-fluid flex-shrink-0 rounded " src="Data/pp.jpg" style="width: 45px; height: 45px;" />
                            <div class="ps-3 ">
                                <h6 class="fw-bold mb-1 ">Client's Name</h6>
                                <small>Client's Gender</small>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Testimonial End -->

</asp:Content>
