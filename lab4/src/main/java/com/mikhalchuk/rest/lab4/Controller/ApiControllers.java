package com.mikhalchuk.rest.lab4.Controller;

import com.mikhalchuk.rest.lab4.Models.User;
import com.mikhalchuk.rest.lab4.Repo.UserRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class ApiControllers {

    @Autowired
    private UserRepo userRepo;
    @GetMapping(value = "/")
    public String getPage() {
        return "Welcome";
    }

    @GetMapping(value = "/users")
    public List<User> getUsers() {
        return userRepo.findAll();
    }

    @PostMapping(value = "/users/add")
    public String addUser(@RequestBody User user) {
        userRepo.save(user);

        return "New User was created";
    }

    @PutMapping(value = "/users/update/{id}")
    public String updateUser(@PathVariable long id, @RequestBody User user) {
        User updatedUser = userRepo.findById(id).get();
        updatedUser.setFirstName(user.getFirstName());
        updatedUser.setLastName(user.getLastName());
        userRepo.save(updatedUser);

        return "New User with id " + id + " was updated";
    }

    @DeleteMapping(value = "/users/delete/{id}")
    public String deleteUser(@PathVariable long id) {
        User deletedUser = userRepo.findById(id).get();
        userRepo.delete(deletedUser);

        return "New User with id " + id + " was deleted";
    }
}
