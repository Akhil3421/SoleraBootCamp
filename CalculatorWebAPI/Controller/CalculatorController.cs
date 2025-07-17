using CalculatorWebAPI.Models;
using CalculatorWebAPI.Services; // Add this to use CalculatorService
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalculatorWebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorsController : ControllerBase
    {
        private readonly CalculatorContext _context;
        private readonly CalculatorService _service;

        public CalculatorsController(CalculatorContext context)
        {
            _context = context;
            _service = new CalculatorService(); // no DI, instantiated directly
        }

        [HttpPost("add")]
        public async Task<IActionResult> Addition(int op1, int op2)
        {
            try
            {
                var calc = _service.Add(op1, op2);
                _context.Calculators.Add(calc);
                await _context.SaveChangesAsync();
                return Ok(calc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("subtract")]
        public async Task<ActionResult> Subtraction(int op1, int op2)
        {
            try
            {
                var calc = _service.Subtract(op1, op2);
                _context.Calculators.Add(calc);
                await _context.SaveChangesAsync();
                return Ok(calc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("multiply")]
        public async Task<ActionResult> Multiplication(int op1, int op2)
        {
            try
            {
                var calc = _service.Multiply(op1, op2);
                _context.Calculators.Add(calc);
                await _context.SaveChangesAsync();
                return Ok(calc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("divide")]
        public async Task<ActionResult> Division(int op1, int op2)
        {
            try
            {
                var calc = _service.Divide(op1, op2);
                _context.Calculators.Add(calc);
                await _context.SaveChangesAsync();
                return Ok(calc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calculator>>> GetCalculators()
        {
            return await _context.Calculators.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Calculator>> GetCalculator(int id)
        {
            var calculator = await _context.Calculators.FindAsync(id);

            if (calculator == null)
            {
                return NotFound();
            }

            return calculator;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalculator(int id, Calculator calculator)
        {
            if (id != calculator.Id)
            {
                return BadRequest();
            }

            _context.Entry(calculator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculatorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Calculator>> PostCalculator(Calculator calculator)
        {
            try
            {
                calculator = _service.Calculate(calculator);
                _context.Calculators.Add(calculator);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCalculator), new { id = calculator.Id }, calculator);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalculator(int id)
        {
            var calculator = await _context.Calculators.FindAsync(id);
            if (calculator == null)
            {
                return NotFound();
            }

            _context.Calculators.Remove(calculator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalculatorExists(int id)
        {
            return _context.Calculators.Any(e => e.Id == id);
        }
    }
}
