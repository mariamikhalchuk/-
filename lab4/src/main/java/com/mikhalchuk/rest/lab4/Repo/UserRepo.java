package com.mikhalchuk.rest.lab4.Repo;

import com.mikhalchuk.rest.lab4.Models.User;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepo extends JpaRepository<User, Long> {
}
