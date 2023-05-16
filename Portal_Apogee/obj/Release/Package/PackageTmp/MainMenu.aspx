﻿<%@ Page Title="MainMenu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="Portal_Apogee.MainMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" ValidateRequestMode="Enabled" ViewStateMode="Enabled">


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <script>
        $(document).ready(function () {
            function disablePrev() { window.history.forward() }
            window.onload = disablePrev();
            window.onpageshow = function (evt) { if (evt.persisted) disableBack() }
        });
    </script>

    <style type="text/css">
        .cards-list {
            z-index: 0;
            width: 100%;
            display: flex;
            justify-content: space-around;
            flex-wrap: wrap;
        }

        .card {
            margin: 30px auto;
            width: 300px;
            height: 300px;
            border-radius: 40px;
            box-shadow: 5px 5px 30px 7px rgba(0,0,0,0.25), -5px -5px 30px 7px rgba(0,0,0,0.22);
            cursor: pointer;
            transition: 0.4s;
        }

            .card .card_image {
                width: inherit;
                height: inherit;
                border-radius: 40px;
            }

                .card .card_image img {
                    width: inherit;
                    height: inherit;
                    border-radius: 40px;
                    object-fit: cover;
                }

            .card .card_title {
                text-align: center;
                border-radius: 0px 0px 40px 40px;
                font-family: sans-serif;
                font-weight: bold;
                font-size: 30px;
                margin-top: -80px;
                height: 40px;
            }

            .card:hover {
                transform: scale(0.9, 0.9);
                box-shadow: 5px 5px 30px 15px rgba(0,0,0,0.25), -5px -5px 30px 15px rgba(0,0,0,0.22);
            }

        .title-white {
            color: white;
        }

        .title-black {
            color: black;
        }

        @media all and (max-width: 500px) {
            .card-list {
                /* On small screens, we are no longer using row direction but column */
                flex-direction: column;
            }
        }

        h1 {
            text-shadow: 0 1px 0 #ccc, 0 2px 0 #c9c9c9, 0 3px 0 #bbb, 0 4px 0 #b9b9b9, 0 5px 0 #aaa, 0 6px 1px rgba(0,0,0,.1), 0 0 5px rgba(0,0,0,.1), 0 1px 3px rgba(0,0,0,.3), 0 3px 5px rgba(0,0,0,.2), 0 5px 10px rgba(0,0,0,.25), 0 10px 10px rgba(0,0,0,.2), 0 20px 20px rgba(0,0,0,.15);
        }

        footer {
            text-align: left;
            position: fixed;
            bottom: 0;
            left: 0
        }
        p{
            color:black;
        }
    </style>
    <h1 style="text-align: center">Portal Apogee</h1>

    <div class="cards-list">
        
        <a href="Upload.aspx" id="Upload" runat="server">
            <div class="card 1">

                <div class="card_image">
                    <%--<img src="https://i.redd.it/b3esnz5ra34y.jpg" />--%>
                    <img src="https://img.icons8.com/office/512/000000/database-restore.png" />
                </div>
                <div class="card_title">
                    <p>Upload Access Codes</p>
                </div>
            </div>
        </a>

        <a href="UnableAc.aspx" id="UnableAC" runat="server">
            <div class="card 2">

                <div class="card_image">
                    <%--<img src="https://img.icons8.com/dusk/512/000000/file-delete.png" />--%>
                    <img src="https://img.icons8.com/officel/512/000000/cancel-subscription.png"/>
                </div>
                <div class="card_title">
                    <p>Re-use Access Code</p>
                </div>
            </div>
        </a>

        <a href="View.aspx" id="view" runat="server">
            <div class="card 3">

                <div class="card_image">
                    <img src="https://img.icons8.com/officel/512/000000/view-shedule.png" />
                </div>
                <div class="card_title">
                    <p>View</p>
                </div>
            </div>
        </a>

     

        <a href="Manto.aspx" id="maintenance" runat="server">
            <div class="card 3">

                <div class="card_image">
                    <img src="https://img.icons8.com/dusk/512/000000/maintenance.png" />
                </div>
                <div class="card_title">
                    <p>Maintenance</p>
                </div>
            </div>
        </a>


    </div>
</asp:Content>
