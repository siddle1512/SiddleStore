﻿using DataAcess.Repository.Order;
using DataAcess.Repository.OrderDetail;
using DataAcess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiddleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;

        public OrderController()
        {
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();
        }


        [Authorize(Roles = "Manager, Employee")]
        [HttpGet]
        public IActionResult GetOrderList()
        {
            try
            {
                var oreders = orderRepository.GetOrderList();
                return Ok(oreders);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Manager, Customer, Employee")]
        [HttpGet("OrderDetail")]
        public IActionResult GetOrderDetail(int orderId)
        {
            try
            {
                var orderDetails = orderDetailRepository.GetOrderDetails(orderId);
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Manager, Customer, Employee")]
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder(OrderViewModel viewNodel)
        {
            try
            {
                orderRepository.CreateOrder(viewNodel);
                return Ok(new { mess = "Add order successfully!" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Manager, Emloyee")]
        [HttpPut("EditOrder")]
        public IActionResult EditOrder(int id, string status)
        {
            try
            {
                orderRepository.Update(id, status);              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(new { mess = "Update Order with the ID " + id + " successfully!" });
        }
    }
}

